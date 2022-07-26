using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Obj
{
    Selfie, Sign, Shoot
}
public class ObjectivesBehaviour : MonoBehaviour
{
    // later
    public Obj objective; 
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //do the objective

        }    
    }
}
