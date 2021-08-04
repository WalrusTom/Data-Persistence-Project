using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public string playerName;
    public int highScore;
    public string highScoreName;

    public TextMeshProUGUI nameInputField;
    public TextMeshProUGUI menuHighScore;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        
        SetHighScoreAwake();
        SetHighScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitButtonClicked()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
Application.Quit();
#endif
    }

    public void StartButtonClicked()
    {
        //get the name entered from the text box

        playerName = nameInputField.text;
        SceneManager.LoadScene(1);
    }

    private void SetHighScoreAwake()
    {
        //Sets the high score on awake. Should attempt to load high score from file, if nothing exists, set high score to 0

        //if high score file exists
        if (File.Exists(Application.persistentDataPath + "/savefile.json"))
        {
            ReadHighScore();
        }

        else
        {
            highScore = 0;
            highScoreName = "";
        }
    }

    [System.Serializable]
    class SaveData
    {
        public int highScore;
        public string highScoreName;
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.highScore = highScore;
        data.highScoreName = highScoreName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    private void ReadHighScore()
    {
        //have already checked if the high score file exists
        string json = File.ReadAllText(Application.persistentDataPath + "/savefile.json");
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        highScore = data.highScore;
        highScoreName = data.highScoreName;

    }

    void SetHighScoreText()
    {
        menuHighScore.text = "Best Score: "+highScoreName+": " + highScore;
    }
}
