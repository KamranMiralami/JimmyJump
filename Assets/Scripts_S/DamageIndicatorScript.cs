using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicatorScript : MonoBehaviour
{
    [SerializeField] GameObject indicatorSample;
    [SerializeField] float maxDistance = 5;
    [SerializeField] float maxScaleMultiplier = 1;
    Vector3 initialScale;
    List<GameObject> guards;
    List<GameObject> indicators;

    // Start is called before the first frame update
    void Start()
    {
        guards = new List<GameObject> (GameObject.FindGameObjectsWithTag("Guard"));
        initialScale = indicatorSample.transform.localScale;
        indicators = new List<GameObject> ();
        for(int i = 0; i < guards.Count; i++)
        {
            indicators.Add(Instantiate(indicatorSample));
            indicators[i].transform.parent = gameObject.transform;
            Vector3 dir = Vector3.Normalize(guards[i].transform.position - transform.parent.position);
            dir.y = 0;
            indicators[i].transform.forward = dir;
            Vector3 pos = transform.position + dir;
            indicators[i].transform.position = pos;
            indicators[i].transform.localScale = scaleOfIndicator(guards[i].transform.position);
        }
    }

    Vector3 scaleOfIndicator(Vector3 pos)
    {
        float scale = (maxDistance - Mathf.Min((pos - transform.parent.position).magnitude, maxDistance)) / maxDistance;
        scale *= maxScaleMultiplier;
        return initialScale * scale;
    }

    // Update is called once per frame
    void Update()
    {
        List<int> temp = new List<int>();
        for (int i = 0; i < guards.Count; i++)
        {
            if (guards[i].GetComponentInChildren<BallImpactHandlerScript>().isDeathPlayed)
            {
                temp.Add(i);
            }
            else
            {
                Vector3 dir = Vector3.Normalize(guards[i].transform.position - transform.parent.position);
                dir.y = 0;
                indicators[i].transform.forward = dir;
                Vector3 pos = transform.position + dir;
                indicators[i].transform.position = pos;
                indicators[i].transform.localScale = scaleOfIndicator(guards[i].transform.position);
            }
        }
        GameObject tempObject;
        for (int i=0; i<temp.Count; i++)
        {
            guards.RemoveAt(temp[i]);
            tempObject = indicators[temp[i]];
            indicators.RemoveAt(temp[i]);
            Destroy(tempObject);
        }
    }

    public void addGuard(GameObject guard)
    {
        guards.Add(guard);
        indicators.Add(Instantiate(indicatorSample));
        indicators[indicators.Count-1].transform.parent = gameObject.transform;
        Vector3 dir = Vector3.Normalize(guard.transform.position - transform.parent.position);
        dir.y = 0;
        indicators[indicators.Count - 1].transform.forward = dir;
        Vector3 pos = transform.position + dir;
        indicators[indicators.Count - 1].transform.position = pos;
        indicators[indicators.Count - 1].transform.localScale = scaleOfIndicator(guard.transform.position);
    }
}
