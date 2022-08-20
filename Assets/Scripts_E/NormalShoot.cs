using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalShoot : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] AudioSource impactAudio;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            /*Vector3 force = (transform.position - _player.transform.position);
            Vector3 tmp = force.normalized;
            float dis = Mathf.Infinity;
            Vector3 dir = new Vector3(1, 1, 1);
            if(Vector3.Distance(tmp,new Vector3(0,0,1))<dis)
            {
                dir = new Vector3(0, 0, 1);
                dis = Vector3.Distance(tmp, new Vector3(0, 0, 1));
            }
            if (Vector3.Distance(tmp, new Vector3(1, 0, 1)) < dis)
            {
                dir = new Vector3(1, 0, 1);
                dis = Vector3.Distance(tmp, new Vector3(1, 0, 1));
            }
            if (Vector3.Distance(tmp, new Vector3(1, 0, 0)) < dis)
            {
                dir = new Vector3(1, 0, 0);
                dis = Vector3.Distance(tmp, new Vector3(1, 0, 0));
            }
            if (Vector3.Distance(tmp, new Vector3(0, 0, -1)) < dis)
            {
                dir = new Vector3(0, 0, -1);
                dis = Vector3.Distance(tmp, new Vector3(0, 0, -1));
            }
            if (Vector3.Distance(tmp, new Vector3(-1, 0, 1)) < dis)
            {
                dir = new Vector3(-1, 0, 1);
                dis = Vector3.Distance(tmp, new Vector3(-1, 0, 1));
            }
            if (Vector3.Distance(tmp, new Vector3(-1, 0, -1)) < dis)
            {
                dir = new Vector3(-1, 0, -1);
                dis = Vector3.Distance(tmp, new Vector3(-1, 0, -1));
            }
            if (Vector3.Distance(tmp, new Vector3(-1, 0, 0)) < dis)
            {
                dir = new Vector3(-1, 0, 0);
                dis = Vector3.Distance(tmp, new Vector3(-1, 0, 0));
            }
            if (Vector3.Distance(tmp, new Vector3(1, 0, -1)) < dis)
            {
                dir = new Vector3(1, 0, -1);
                dis = Vector3.Distance(tmp, new Vector3(1, 0, -1));
            }
            Debug.LogWarning(force.normalized);
            Debug.LogWarning(dir);
            //GetComponent<Rigidbody>().AddForce(dir* 1500f * Time.deltaTime);*/

            GetComponent<Rigidbody>().AddForce((transform.position - _player.transform.position) * 1500f * Time.deltaTime);
            impactAudio.Play();
            //GetComponent<Rigidbody>().AddForce(transform.up * 500f * Time.deltaTime);
        }
        else if (other.gameObject.CompareTag("Guard"))
        {
            impactAudio.Play();
        }
    }
}
