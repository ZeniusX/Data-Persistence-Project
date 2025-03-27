using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using System.IO;



#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string CurrentPlayerName;
    public string BestPlayerName = "None";
    public int BestPlayerScore = 0;
    
    [System.Serializable]
    private class ScoreData
    {
        public string PlayerName;
        public int PlayerScore;
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        LoadGame();
    }

    public void InputName(string name)
    {
        CurrentPlayerName = name;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void SaveGame(int Score)
    {
        ScoreData SaveData = new ScoreData();
        SaveData.PlayerName = CurrentPlayerName;
        SaveData.PlayerScore = Score;

        string json = JsonUtility.ToJson(SaveData);

        File.WriteAllText(Application.persistentDataPath + "/DataPersistenceSave.json", json);
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/DataPersistenceSave.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            ScoreData LoadData = JsonUtility.FromJson<ScoreData>(json);
            BestPlayerName = LoadData.PlayerName;
            BestPlayerScore = LoadData.PlayerScore;
        }
    }
}
