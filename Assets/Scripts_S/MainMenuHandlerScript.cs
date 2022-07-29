using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandlerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        HighScores highScores = new HighScores();
        highScores.names = new string[] { "me", "you", "them" };
        highScores.scores = new int[] { 1, 2, 3 };
        PlayerPrefs.SetString("scores", JsonUtility.ToJson(highScores));
    }

    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
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
