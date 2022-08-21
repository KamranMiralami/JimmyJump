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
    // later
    private void Start()
    {
        
    }

    public Obj objective; 
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gameHnadler.GetComponent<GameHandlerScript>().disableGuardsAndCompass();
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
