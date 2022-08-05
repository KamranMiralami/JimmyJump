using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SurviveObjective : MonoBehaviour
{
    public GameObject damageIndicatorHandler;
    public GameObject WinTrigger;
    public int duration;
    public TextMeshProUGUI surviveText;
    public GameObject tutorialMessage;
    public int spawnInterval = 3;
    public GameObject player;
    GameObject upperSpawn;
    GameObject lowerSpawn;
    public GameObject Guard;
    private void Start()
    {
        WinTrigger.SetActive(false);
        upperSpawn = transform.GetChild(0).gameObject;
        lowerSpawn = transform.GetChild(1).gameObject;
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
            if (duration % spawnInterval==0)
            {
                SpawnGuard();
            }
        }
        yield return new WaitForSeconds(0.5f);
        surviveText.gameObject.SetActive(false);
        if(tutorialMessage!=null)
            StartCoroutine(ShowExitMessage(tutorialMessage));
    }
    public IEnumerator ShowExitMessage(GameObject go)
    {
        WinTrigger.SetActive(true);
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
    public void SpawnGuard()
    {
        GameObject go;
        if (player.transform.position.z < -12)
        {
            go=Instantiate(Guard, upperSpawn.transform.position, upperSpawn.transform.rotation);
        }
        else
        {
            go=Instantiate(Guard, lowerSpawn.transform.position, lowerSpawn.transform.rotation);
        }
        go.GetComponent<GuardBehaviour>().chasingGO = player;
        damageIndicatorHandler.GetComponent<DamageIndicatorScript>().addGuard(go);
    }
}
