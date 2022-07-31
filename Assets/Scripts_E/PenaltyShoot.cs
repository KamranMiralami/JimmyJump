using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PenaltyShoot : MonoBehaviour
{
    [SerializeField] private GameObject Target;
    private Boolean ableShoot = true;
    [SerializeField] private GameObject goal;
    [SerializeField] private float power = 50f;
    public PlayerMove pm;
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && ableShoot)
        {
            Shoot();
            ableShoot = false;
            
            
        }
    }
    private IEnumerator wait()
    {

        yield return new WaitForSeconds(2f);
        pm.EnableMoving();
        Target.SetActive(false);
        this.gameObject.SetActive(false);
        goal.GetComponent<GameHandlerScript>().cameraFollow();
    }

    private void Start()
    {
        pm.DisableMoving();
        Target.SetActive(true);
        Target.transform.DOMove(new Vector3(-46, 4, 8), 2.5f).SetEase(Ease.InOutSine).SetLoops(-1,LoopType.Yoyo);
    }
    private void Shoot()
    {
        Vector3 shoot = (Target.transform.position - transform.position).normalized;
        GetComponent<Rigidbody>().AddForce(shoot * power,ForceMode.Impulse);
        StartCoroutine(wait());
    }
}
