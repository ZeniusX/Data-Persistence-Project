using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    [SerializeField] TMP_InputField nameField;
    [SerializeField] TMP_Text bestScoreText;

    void Update()
    {
        bestScoreText.text = $"Best Score : {GameManager.gameManager.hsPlayerName} : {GameManager.gameManager.highScore}";
    }

    public void StartScene()
    {
        GameManager.gameManager.cPlayerName = nameField.text;
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        GameManager.gameManager.SaveScore();

        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }
}
