using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIHandler : MonoBehaviour
{
    public GameManager gameManager;
    // Start is called before the first frame update
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void startNew()
    {
        if (gameManager.nameField.text != "")
        {
            gameManager.name = gameManager.nameField.text;
            SceneManager.LoadScene(1);
        }
    }

    public void Exit()
    {
        gameManager.SaveGameInfo();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
