using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KnockOutBehaviour : MonoBehaviour
{
    public GameObject Message;
    public TextMeshProUGUI ShootText;
    public int numberOfHits=0;
    public int goal=1;
    public void Hit()
    {
        if (numberOfHits < goal)
        {
            numberOfHits++;
            ShootText.text=numberOfHits.ToString();
        }
        else
        {
            return;
        }
        if (numberOfHits >= goal) {
            if(Message!=null) StartCoroutine(ShowExitMessage(Message));
        }
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
        Message.SetActive(false);
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
