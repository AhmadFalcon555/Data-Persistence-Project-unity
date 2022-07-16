using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public Text bestScoreText;
    public InputField nameField;

    public string bestName;
    public int bestScore;

    public static GameManager Instance;
    //yang disimpan
    public string name = "";

    // Start is called before the first frame update

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadGameInfo();
    }

    private void Start()
    {
        if (bestName != "") // start pertama kali, kalo emang ada yang bisa diload, bakal dimunculin dibawah
        {
            bestScoreText.text = "Best Score : " + bestName + " : " + bestScore;
        }
    }

    public void SetBestScore(int score)
    {
        if(score > bestScore)
        {
            bestScore = score;
            bestName = name;

            MainManager.mainManager.FinalScore.text = "Best Score : " + bestName + " : " + bestScore;
        }
    }

    public void startNew()
    {
        if(nameField.text != "")
        {
            name = nameField.text;
            SceneManager.LoadScene(1);
        }
    }

    public void Exit()
    {
        SaveGameInfo();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    [System.Serializable]
    class SaveData
    {
        public string name;
        public int bestScore;
    }

    public void SaveGameInfo()
    {
        SaveData data = new SaveData();
        data.name = bestName;
        data.bestScore = bestScore;

        string JSON = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", JSON);
    }

    public void LoadGameInfo()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestName = data.name;
            bestScore = data.bestScore;
        }
    }
}


