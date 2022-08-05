using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternHandler : MonoBehaviour
{
    [SerializeField] public GameObject[] patterns;
    public int num = 0;
    public bool finished;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(finished)
        {
            return;
        }
        if (patterns[num].GetComponent<PatternsBehaviour>().triggered==true)
        {
            num++; 
            if (num >= patterns.Length)
            {
                finished = true;
            }
        }
    }
}
