using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SurviveObjective : MonoBehaviour
{
    public int duration;
    public TextMeshProUGUI surviveText;
    public GameObject tutorialMessage;
    private void Start()
    {
        surviveText.text = duration.ToString();
        StartCoroutine(startCounter());
    }
    IEnumerator startCounter()
    {
        yield return new WaitForSeconds(2);
        if(surviveText.gameObject.activeSelf==false)
        {
            surviveText.gameObject.SetActive(true);
        }
        while (duration > 0)
        {
            yield return new WaitForSeconds(1);
            duration--;
            surviveText.text = duration.ToString();
        }
        yield return new WaitForSeconds(0.5f);
        surviveText.gameObject.SetActive(false);
        if(tutorialMessage!=null)
            StartCoroutine(ShowExitMessage(tutorialMessage));
    }
    public IEnumerator ShowExitMessage(GameObject go)
    {
        if (go.activeSelf == false)
        {
            go.SetActive(true);
        }
        Vector3 start = go.transform.GetChild(0).transform.position;
        Vector3 end = go.transform.GetChild(1).transform.position;
        go.transform.position = start;
        float durationStart = 0;
        while (go.transform.position != end)
        {
            yield return new WaitForSeconds(0.02f);
            durationStart += 0.02f;
            go.transform.position = Vector3.Lerp(start, end, durationStart / 0.1f);
        }
        yield return new WaitForSeconds(5);
        durationStart = 0f;
        while (go.transform.position != start)
        {
            yield return new WaitForSeconds(0.02f);
            durationStart += 0.02f;
            go.transform.position = Vector3.Lerp(end, start, durationStart / 1);
        }
    }
}
