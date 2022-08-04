using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PenaltyShoot : MonoBehaviour
{
    [SerializeField] private GameObject Target;
    [SerializeField] private GameObject Target2;
    private Boolean ableShoot = true;
    [SerializeField] private Animator WinOrLose;
    [SerializeField] private GameObject goal;
    [SerializeField] private float power = 50f;
    public PlayerMove pm;
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && ableShoot)
        {
            Target.transform.DOKill(true);
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
        var position = Target2.transform.position;
        Target.transform.DOMove(new Vector3
            (position.x, position.y, position.z), 2f)
            .SetEase(Ease.InOutSine).SetLoops(-1,LoopType.Yoyo);
    }
    private void Shoot()
    {
        
        Vector3 shoot = (Target.transform.position - transform.position).normalized;
        GetComponent<Rigidbody>().AddForce(shoot * power,ForceMode.Impulse);
        StartCoroutine(wait());
    }
}
