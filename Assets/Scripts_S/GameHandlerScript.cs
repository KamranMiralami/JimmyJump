using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameHandlerScript : MonoBehaviour
{
    [SerializeField] GameObject[] menuObjects;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
