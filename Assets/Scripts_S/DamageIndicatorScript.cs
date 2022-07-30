using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicatorScript : MonoBehaviour
{
    [SerializeField] GameObject indicatorSample;
    [SerializeField] float maxDistance = 5;
    GameObject[] guards;
    GameObject[] indicators;

    // Start is called before the first frame update
    void Start()
    {
        guards = GameObject.FindGameObjectsWithTag("Guard");
        indicators = new GameObject[guards.Length];
        for(int i = 0; i < guards.Length; i++)
        {
            indicators[i] = Instantiate(indicatorSample);
            indicators[i].transform.parent = gameObject.transform;
            indicators[i].GetComponent<Renderer>().material.color = Color.red;
            Vector3 dir = Vector3.Normalize(guards[i].transform.position - transform.parent.position);
            dir.y = 0;
            indicators[i].transform.forward = dir;
            Vector3 pos = transform.position + dir;
            indicators[i].transform.position = pos;
            float scale = scaleOfIndicator(guards[i].transform.position);
            indicators[i].transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    float scaleOfIndicator(Vector3 pos)
    {
        return (maxDistance - Mathf.Min((pos - transform.parent.position).magnitude, maxDistance)) / maxDistance;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < guards.Length; i++)
        {
            Vector3 dir = Vector3.Normalize(guards[i].transform.position - transform.parent.position);
            dir.y = 0;
            indicators[i].transform.forward = dir;
            Vector3 pos = transform.position + dir;
            indicators[i].transform.position = pos;
            float scale = scaleOfIndicator(guards[i].transform.position);
            indicators[i].transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
