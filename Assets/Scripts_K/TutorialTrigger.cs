using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;

public class TutorialTrigger : MonoBehaviour
{
    public GameObject tutorialText;
    bool triggered = false;
    public float delay = 0f;
    private void OnTriggerEnter(Collider other)
    {
        if(!triggered)
            if (other.gameObject.CompareTag("Player"))
            {
                triggered = true;
                StartCoroutine(ShowTutorialMessage(tutorialText));
            }
    }
    public float tutorialPivotDuration;
    public float tutorialStayDuration;
    public float tutorialPivotBackDuration;
    public IEnumerator ShowTutorialMessage(GameObject go)
    {
        yield return new WaitForSeconds(delay);
        if (go.activeSelf==false)
        {
            go.SetActive(true);
        }
        if(go.GetComponent<Image>()!=null)
        {
            go = go.transform.GetChild(0).gameObject;
        }
        Vector3 start = go.transform.GetChild(0).transform.position;
        Vector3 end = go.transform.GetChild(1).transform.position;
        go = go.transform.parent.gameObject;
        go.transform.position = start;
        float durationStart = 0;
        while (go.transform.position != end)
        {
            yield return new WaitForSeconds(0.02f);
            durationStart += 0.02f;
            go.transform.position = Vector3.Lerp(start, end, durationStart / tutorialPivotDuration);
        }
        if (tutorialStayDuration >= 0)
        {
            yield return new WaitForSeconds(tutorialStayDuration);
            durationStart = 0f;
            while (go.transform.position != start)
            {
                yield return new WaitForSeconds(0.02f);
                durationStart += 0.02f;
                go.transform.position = Vector3.Lerp(end, start, durationStart / tutorialPivotBackDuration);
            }
        }
    }
}
