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
            GetComponentInChildren<RangeRender>().DrawCircle();
        }
    }
    private void Update()
    {
        if(chasingGO != null)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                chasingGO.transform.position + new Vector3(0, transform.localScale.y,0),
                speed * Time.deltaTime
                );
            transform.forward = chasingGO.transform.position - transform.position;
        }
    }
}
