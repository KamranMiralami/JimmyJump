using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PenaltyShoot : MonoBehaviour
{
    [SerializeField] private GameObject Target;
    [SerializeField] private GameObject objective;
    [SerializeField] private GameObject Target2;
    private Boolean ableShoot = true, AbleDance = false;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Win"))
        {
            AbleDance = true;
            Debug.Log("goal");
        }
    }


    private IEnumerator wait()
    {
        
        
        yield return new WaitForSeconds(2f);
        if (AbleDance)
        {
            pm.EnableMoving();
            goal.GetComponent<GameHandlerScript>()
                .focusCamera(transform.GetChild(1).transform.position,
                    transform.GetChild(1).transform.rotation);
            WinOrLose.SetBool("isDancing",true);
        }
        else
        {
            pm.EnableMoving();
            goal.GetComponent<GameHandlerScript>().cameraFollow();
        }

        Target.SetActive(false);
        objective.SetActive(false);
        this.gameObject.SetActive(false);
        
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
