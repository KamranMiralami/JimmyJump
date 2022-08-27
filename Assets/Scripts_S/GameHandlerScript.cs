using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameHandlerScript : MonoBehaviour
{
    [SerializeField] GameObject[] menuObjects;
    [SerializeField] GameObject youLoseText;
    [SerializeField] GameObject youWinText;
    [SerializeField] GameObject mainCanvas;
    [SerializeField] float textAnimationDuration = 0.5f;
    [SerializeField] float textDelayToMenu = 2.5f;
    [SerializeField] AudioSource loseAudio;
    [SerializeField] AudioSource winAudio;
    Quaternion cameraQuaternion;
    bool compassNeeded = false;

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
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        GameAnalyticsSDK.GameAnalytics.Initialize();
        
    }

    public void cameraUnfollow()
    {
        cameraQuaternion = Camera.main.transform.rotation;
        Camera.main.GetComponent<CameraFollow>().isFollowing = false;
    }

    public void cameraFollow()
    {
        Camera.main.transform.rotation = cameraQuaternion;
        Camera.main.GetComponent<CameraFollow>().isFollowing = true;
    }

    public void focusCamera(Vector3 position, Quaternion rotation)
    {
        cameraUnfollow();
        StartCoroutine(moveCamera(position, rotation, 1));
    }

    public IEnumerator moveCamera(Vector3 position, Quaternion rotation, float duration)
    {
        Vector3 startingPosition = Camera.main.transform.position;
        Quaternion startingRotation = Camera.main.transform.rotation;
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / duration;
            Camera.main.transform.position = Vector3.Lerp(startingPosition, position, t);
            Camera.main.transform.rotation = Quaternion.Lerp(startingRotation, rotation, t);
            yield return null;
        }
        t = 0f;
        // while (t < 5)
        // {
        //     t += Time.deltaTime / duration;
        //     yield return null;
        // }
        // cameraFollow();
    }

    public void lose()
    {
        disableGuardsAndCompass();
        GameObject text = Instantiate(youLoseText, mainCanvas.transform);
        loseAudio.Play();
        StartCoroutine(textRoutine(text));
    }

    IEnumerator textRoutine(GameObject loseText)
    {
        float t = 0f;
        Vector3 initialPos = loseText.transform.localPosition;
        loseText.transform.localPosition = new Vector3(
                initialPos.x,
                900,
                initialPos.z);
        while (t < 1)
        {
            t += Time.deltaTime / textAnimationDuration;
            loseText.transform.localPosition = new Vector3(
                initialPos.x,
                Mathf.Lerp(900, initialPos.y, t),
                initialPos.z);
            yield return null;
        }
        t = 0f;
        while(t < 1)
        {
            t += Time.deltaTime / textDelayToMenu;
            yield return null;
        }
        SceneManager.LoadScene("MainMenu");
    }

    public void win()
    {
        disableGuardsAndCompass();
        GameObject text = Instantiate(youWinText, mainCanvas.transform);
        winAudio.Play();
        StartCoroutine(textRoutine(text));
        int level = int.Parse(SceneManager.GetActiveScene().name.Substring(5));
        int playerLevel = PlayerPrefs.GetInt("currentLevel");
        if(playerLevel < level)
        {
            PlayerPrefs.SetInt("currentLevel", level);
        }
    }

    public void disableGuardsAndCompass()
    {
        GameObject[] guards = GameObject.FindGameObjectsWithTag("Guard");
        for (int i = 0; i < guards.Length; i++)
        {
            guards[i].GetComponent<GuardBehaviour>().stop();
        }
        disableCompass();
    }

    public void disableCompass()
    {
        GameObject[] indicators = GameObject.FindGameObjectsWithTag("Indicator");
        if (indicators != null)
        {
            for (int i = 0; i < indicators.Length; i++)
            {
                indicators[i].SetActive(false);
            }
        }
        GameObject compass = GameObject.FindGameObjectWithTag("Compass");
        if (compass != null)
        {
            if (compass.activeSelf)
            {
                compassNeeded = true;
            }
            MeshRenderer[] meshRenderers = compass.GetComponentsInChildren<MeshRenderer>();
            for(int i=0; i<meshRenderers.Length; i++)
            {
                meshRenderers[i].enabled = false;
            }
        }
    }

    public void enableCompassIfNeeded()
    {
        GameObject[] indicators = GameObject.FindGameObjectsWithTag("Indicator");
        if (indicators != null)
        {
            for (int i = 0; i < indicators.Length; i++)
            {
                indicators[i].SetActive(true);
            }
        }
        GameObject compass = GameObject.FindGameObjectWithTag("Compass");
        if (compass != null)
        {
            if (compassNeeded)
            {
                MeshRenderer[] meshRenderers = compass.GetComponentsInChildren<MeshRenderer>();
                for (int i = 0; i < meshRenderers.Length; i++)
                {
                    meshRenderers[i].enabled = true;
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
