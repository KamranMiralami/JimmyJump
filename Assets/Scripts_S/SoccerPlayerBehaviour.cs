using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoccerPlayerBehaviour : MonoBehaviour
{
    [SerializeField] GameObject gameHandler;
    [SerializeField] GameObject player;
    [SerializeField] Image selfieImage;
    [SerializeField] Image tapImage;
    [SerializeField] float selfieDuration = 1.5f;
    [SerializeField] float selfiePopupDuration = 0.5f;
    [SerializeField] RectTransform mainCanvasRect;
    bool isTriggered = false;
    bool countingTaps = false;
    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            gameHandler.GetComponent<GameHandlerScript>().disableGuardsAndCompass();
            isTriggered = true;
            player.GetComponent<PlayerMove>().DisableMoving();
            selfieImage.gameObject.SetActive(true);
            countingTaps = true;
            StartCoroutine(selfieRoutine());
        }
    }

    IEnumerator selfieRoutine()
    {
        float t = 0f;
        float initialY = selfieImage.transform.localPosition.y;
        while (t < 1)
        {
            t += Time.deltaTime / selfiePopupDuration;
            selfieImage.transform.localPosition = new Vector3(
                selfieImage.transform.localPosition.x,
                Mathf.Lerp(initialY, 0, t),
                selfieImage.transform.localPosition.z);
            yield return null;
        }
        t = 0f;
        while(t < 1)
        {
            t += Time.deltaTime / selfieDuration;
            yield return null;
        }
        countingTaps = false;
        selfieImage.gameObject.SetActive(false);
        gameHandler.GetComponent<GameHandlerScript>().win();
    }

    IEnumerator tapRoutine(Vector3 pos, float duration)
    {
        float t = 0f;
        tapImage.gameObject.SetActive(true);
        tapImage.transform.localPosition = pos;
        while (t < 1)
        {
            t += Time.deltaTime / duration;
            yield return null;
        }
        tapImage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (countingTaps)
        {
            if(Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                if(touch.phase == TouchPhase.Began)
                {
                    Vector2 localPos;
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(mainCanvasRect, new Vector3(touch.position.x, touch.position.y, 0), null, out localPos);
                    StartCoroutine(tapRoutine(localPos, 0.1f));
                }
                else if(touch.phase == TouchPhase.Ended)
                {
                    score++;
                }
            }
        }
    }
}
