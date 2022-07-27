using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class HighScores
{
    public string[] names;
    public int[] scores;
}

public class ScoresMenuHandlerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string data = PlayerPrefs.GetString("scores", "");
        HighScores highScores = JsonUtility.FromJson<HighScores>(data);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
