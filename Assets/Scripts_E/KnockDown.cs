using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KnockDown : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float speed;
    [SerializeField] private Animator SoccerBehaviour;

    private bool stop = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerTarget"))
        {
            stop = true;
            SoccerBehaviour.SetTrigger("Stand");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }
}
