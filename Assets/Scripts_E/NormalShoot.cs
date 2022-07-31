using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalShoot : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<Rigidbody>().AddForce((transform.position - _player.transform.position) * 1700f * Time.deltaTime);
            GetComponent<Rigidbody>().AddForce(transform.up * 1800f * Time.deltaTime);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
