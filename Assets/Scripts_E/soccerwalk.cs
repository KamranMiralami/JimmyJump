using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class soccerwalk : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float speed;
    [SerializeField] private Animator SoccerBehaviour;
    private bool stop = false;
    private float turnSpeed = 0.75f;
    private Vector3 dir, rotation;
    private Quaternion lookRotation, partToRotate;

     void Start()
    {
       
        
    }

    void Update()
    {
        dir = target.transform.position - transform.position;
        lookRotation = Quaternion.LookRotation(dir);
        rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        
        if (!stop)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerTarget"))
        {
            stop = true;
            SoccerBehaviour.SetTrigger("Stand");
        }
    }
}

