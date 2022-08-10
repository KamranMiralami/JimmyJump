using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuHandlerScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelNumber;
    [SerializeField] int maxLevel;
    int level = 0;
    // Start is called before the first frame update
    void Start()
    {
        HighScores highScores = new HighScores();
        highScores.names = new string[] { "me", "you", "them" };
        highScores.scores = new int[] { 1, 2, 3 };
        PlayerPrefs.SetString("scores", JsonUtility.ToJson(highScores));
        levelNumber.text = "1";
    }

    public void Levels()
    {
        SceneManager.LoadScene("Levels");
    }

    public void Play()
    {
        SceneManager.LoadScene("Level" + (level + 1));
    }

    public void Next()
    {
        level = (level + 1) % maxLevel;
        levelNumber.text = "" + (level + 1);
    }

    public void Previous()
    {
        level = (level - 1) % maxLevel;
        if (level < 0)
        {
            level += maxLevel;
        }
        levelNumber.text = "" + (level + 1);
    }

    public void LoadHighScores()
    {
        SceneManager.LoadScene("HighScores");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
