using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class TutorialTrigger : MonoBehaviour
{
    public GameObject tutorialText;
    bool triggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if(!triggered)
            if (other.gameObject.CompareTag("Player"))
            {
                triggered = true;
                tutorialText.SetActive(true);
                StartCoroutine(ShowTutorialMessage(tutorialText));
            }
    }
    public float tutorialPivotDuration;
    public float tutorialStayDuration;
    public float tutorialPivotBackDuration;
    public IEnumerator ShowTutorialMessage(GameObject go)
    {
        if(go.activeSelf==false)
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
            go.transform.position = Vector3.Lerp(start, end, durationStart / tutorialPivotDuration);
        }
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
