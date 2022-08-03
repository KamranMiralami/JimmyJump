using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenuHandlerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
