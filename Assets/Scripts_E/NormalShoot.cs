using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalShoot : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] AudioSource impactAudio;
    [SerializeField] float ballMinSpeed = 3f;
    [SerializeField] float minTimeBetweenSounds = 0.3f;
    float lastTime;
    bool firstImpact = false;
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
            if(!firstImpact)
            {
                impactAudio.Play();
                firstImpact = true;
                lastTime = Time.time;
            }
            else if(Time.time - lastTime >= minTimeBetweenSounds)
            {
                impactAudio.Play();
                lastTime = Time.time;
            }
            //GetComponent<Rigidbody>().AddForce(transform.up * 500f * Time.deltaTime);
        }
        else if (other.gameObject.CompareTag("Guard"))
        {
            if (gameObject.GetComponent<Rigidbody>().velocity.magnitude >= ballMinSpeed)
            {
                if(!other.gameObject.GetComponentInChildren<BallImpactHandlerScript>().isDeathPlayed)
                {
                    impactAudio.Play();
                }
            }
        }
    }
}
