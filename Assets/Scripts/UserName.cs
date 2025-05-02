using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class UserName : MonoBehaviour
{
    public static UserName Instance;

    public TMP_InputField NameInput;

    public string userName = "Mike";

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        GameObject inputFieldObject = GameObject.Find("Name InputField");
        NameInput = inputFieldObject.GetComponent<TMP_InputField>();
        LoadName();
    }

    [System.Serializable]
    class SaveData
    {
        public string name;
    }

    
    public void SaveName()
    {
        if (NameInput.text != null)
        {
            userName = NameInput.text;
        }
        else
        {
            userName = "Mike";
        }
            
        SaveData data = new SaveData();
        data.name = userName;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/playername.json", json);
    }

    public void LoadName()
    {
        string path = Application.persistentDataPath + "/playername.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            userName = data.name;
            NameInput.text = userName;
        }
    }
}
