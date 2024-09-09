using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;
using System.IO;
using System.Security.Policy;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public string cPlayerName = "Name";
    public string hsPlayerName = "Name";
    public int currentScore = 0;
    public int highScore = 0;
    
    public static GameManager gameManager;

    void Awake()
    {
        if (gameManager != null)
        {
            Destroy(gameObject);
            return;
        }

        gameManager = this;
        DontDestroyOnLoad(gameObject);
        LoadScore();
    }

    void Update()
    {
        if (currentScore > highScore)
        {
            hsPlayerName = cPlayerName;
            highScore = currentScore;
        }
    }

    [Serializable]
    class SaveData
    {
        public string hsPlayerName;
        public int highScore;
    }

    public void SaveScore()
    {
        SaveData sData = new SaveData();
        sData.hsPlayerName = hsPlayerName;
        sData.highScore = highScore;

        string saveFile = JsonUtility.ToJson(sData);

        File.WriteAllText(Application.persistentDataPath + "/persistence.json", saveFile);
    }
    
    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/persistence.json";
        if (File.Exists(path))
        {
            string loadFile = File.ReadAllText(path);
            SaveData lData = JsonUtility.FromJson<SaveData>(loadFile);

            hsPlayerName = lData.hsPlayerName;
            highScore = lData.highScore;
        }
    }
}
