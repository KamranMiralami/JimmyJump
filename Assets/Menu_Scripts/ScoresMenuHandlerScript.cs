using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using TMPro;

[Serializable]
public class HighScores
{
    public string[] names;
    public int[] scores;
}

public class ScoresMenuHandlerScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoresText;

    // Start is called before the first frame update
    void Start()
    {
        string data = PlayerPrefs.GetString("scores", "");
        HighScores highScores = JsonUtility.FromJson<HighScores>(data);
        setScoresList(highScores);
    }

    void setScoresList(HighScores highScores)
    {
        string text = "";
        for(int i=0; i<highScores.names.Length; i++)
        {
            text += (highScores.names[i] + " --> " + highScores.scores[i] + "\n");
        }
        scoresText.text = text;
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
