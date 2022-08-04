using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameHandlerScript : MonoBehaviour
{
    [SerializeField] GameObject[] menuObjects;
    [SerializeField] GameObject youLoseText;
    [SerializeField] GameObject mainCanvas;
    [SerializeField] float loseAnimationDuration = 0.5f;
    [SerializeField] float loseDelayToMenu = 1.5f;
    [SerializeField] AudioSource loseAudio;
    [SerializeField] AudioSource winAudio;
    Quaternion cameraQuaternion;

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
        GameObject text = Instantiate(youLoseText, mainCanvas.transform);
        loseAudio.Play();
        StartCoroutine(loseRoutine(text));
    }

    IEnumerator loseRoutine(GameObject loseText)
    {
        float t = 0f;
        Vector3 initialPos = loseText.transform.localPosition;
        loseText.transform.localPosition = new Vector3(
                initialPos.x,
                900,
                initialPos.z);
        while (t < 1)
        {
            t += Time.deltaTime / loseAnimationDuration;
            loseText.transform.localPosition = new Vector3(
                initialPos.x,
                Mathf.Lerp(900, initialPos.y, t),
                initialPos.z);
            yield return null;
        }
        t = 0f;
        while(t < 1)
        {
            t += Time.deltaTime / loseDelayToMenu;
            yield return null;
        }
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
