using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenuHandlerScript : MonoBehaviour
{
    [SerializeField] Button[] levels;
    int currentLevel;
    // Start is called before the first frame update
    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("currentLevel", 0);
        for(int i=0; i<=currentLevel && i<levels.Length; i++)
        {
            levels[i].enabled = true;
            ColorBlock temp = levels[i].colors;
            temp.normalColor = Color.white;
            levels[i].colors = temp;
        }
        for(int i=currentLevel+1; i<levels.Length; i++)
        {
            levels[i].enabled = false;
            ColorBlock temp = levels[i].colors;
            temp.normalColor = Color.gray;
            levels[i].colors = temp;
        }
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
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
