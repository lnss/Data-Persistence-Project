using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif


// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]


public class MenuUIHandler : MonoBehaviour
{
    public Text BestScoreText;

    public static string bestName = "Mike";
    public static int bestScore = 0;

    public void Start()
    {
        UpdateBestScore();
    }

    [System.Serializable]
    class SaveData
    {
        public string name;
        public int score;
    }

    public void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/bestscore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            bestName = data.name;
            bestScore = data.score;
        }
    }

    public void SaveBestScore()
    {
        SaveData data = new SaveData();
        data.name = bestName;
        data.score = bestScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/bestscore.json", json);
    }

    public void UpdateBestScore()
    {
        LoadBestScore();
        if (UserScore.Instance != null)
        {
            if (UserScore.Instance.userScore > bestScore)
            {
                bestName = UserName.Instance.userName;
                bestScore = UserScore.Instance.userScore;
            }
            SaveBestScore();
        }
        BestScoreText.text = $"Best Score: {bestName}  {bestScore}";
    }


    public void GameStart()
    {
        UserName.Instance.SaveName();
        SceneManager.LoadScene(1);
        
    }

    public void GameExit()
    {

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    

}
