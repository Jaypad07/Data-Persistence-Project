using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TMP_InputField playerInput;
    public string playerName;
    public string HighScoreName;
    public int HighScoreNum;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighScore();
    }

    public void Start()
    {
        TextMeshProUGUI item = GameObject.Find("High Score").GetComponent<TextMeshProUGUI>();

        if (item != null)
        {
            item.text = $"HighScore: {HighScoreName} : {HighScoreNum}";
        }
    }

    public void StartGame()
    {
        playerName = playerInput.text;
        SceneManager.LoadScene(1);
    }

    [System.Serializable]
    class SaveData
    {
        public string HighScoreName;
        public int HighScoreNum;
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.HighScoreName = HighScoreName;
        data.HighScoreNum = HighScoreNum;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            HighScoreName = data.HighScoreName;
            HighScoreNum = data.HighScoreNum;
        }
    }
}
