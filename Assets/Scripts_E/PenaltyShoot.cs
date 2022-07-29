using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PenaltyShoot : MonoBehaviour
{
    [SerializeField] private GameObject Target;
    private Boolean ableShoot = true;
    [SerializeField] private float power = 50f;
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && ableShoot)
        {
            Shoot();
            ableShoot = false;
            
            
        }
    }

    private void Start()
    {
        Target.transform.DOMove(new Vector3(-46, 4, 8), 2.5f).SetEase(Ease.InOutSine).SetLoops(-1,LoopType.Yoyo);
    }

    private void Shoot()
    {
        Vector3 shoot = (Target.transform.position - transform.position).normalized;
        GetComponent<Rigidbody>().AddForce(shoot * power,ForceMode.Impulse);
    }
}
