using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Obj
{
    Selfie, Sign, Shoot
}
public class ObjectivesBehaviour : MonoBehaviour
{
    [SerializeField] GameObject gameHnadler;
    [SerializeField] private GameObject Ball;

    [SerializeField] private GameObject[] guards;
    // later
    private void Start()
    {
        guards = GameObject.FindGameObjectsWithTag("Guard");
    }

    public Obj objective; 
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            for (int i = 0; i < guards.Length; i++)
            {
                guards[i].GetComponent<GuardBehaviour>().stop();
            }
            //do the objective
            gameHnadler.GetComponent<GameHandlerScript>()
                .focusCamera(
                transform.GetChild(0).transform.position,
                transform.GetChild(0).transform.rotation);
            GetComponent<MeshRenderer>().enabled = false;
            Ball.GetComponent<PenaltyShoot>().enabled = true;
        }    
    }
}
