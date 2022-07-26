using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GuardBehaviour : MonoBehaviour
{
    GameObject chasingGO = null;
    public float speed;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("player in " + gameObject.name + " range ");
            chasingGO = other.gameObject;
        }
    }
    private void Update()
    {
        if(chasingGO != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, chasingGO.transform.position, speed*Time.deltaTime);
            transform.forward = chasingGO.transform.position - transform.position;
        }
    }
}
