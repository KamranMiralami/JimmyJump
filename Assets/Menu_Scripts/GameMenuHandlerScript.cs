using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuHandlerScript : MonoBehaviour
{
    [SerializeField] GameObject[] menuObjects;

    // Start is called before the first frame update
    void Start()
    {
        resumeGame();
    }

    public void pauseGame()
    {
        Time.timeScale = 0;
        for(int i=0; i<menuObjects.Length; i++)
        {
            menuObjects[i].SetActive(true);
        }
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
        for (int i = 0; i < menuObjects.Length; i++)
        {
            menuObjects[i].SetActive(false);
        }
    }

    public void toMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
