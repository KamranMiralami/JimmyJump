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
    [SerializeField] private GameObject joystick;
    [SerializeField] private GameObject winorlosecam;
    [SerializeField] private float power = 50f;
    [SerializeField] private GameObject player;
    [SerializeField] private GameHandlerScript gameHandlerScript;
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
            
            joystick.SetActive(false);
            goal.GetComponent<GameHandlerScript>()
                .focusCamera(winorlosecam.transform.position,
                    winorlosecam.transform.rotation);
            WinOrLose.SetBool("isDancing",true);
            Debug.Log("goal");
            StartCoroutine(won());
        }
        if (other.CompareTag("Lose"))
        {
            joystick.SetActive(false);
            goal.GetComponent<GameHandlerScript>()
                .focusCamera(winorlosecam.transform.position,
                    winorlosecam.transform.rotation);
            WinOrLose.SetBool("isLost",true);
            Debug.Log("not goal");
            StartCoroutine(lost());
        }
    }


    private IEnumerator wait()
    {
        yield return new WaitForSeconds(2f);
        Target.SetActive(false);
        objective.SetActive(false);
        gameObject.GetComponent<MeshRenderer>().enabled = false;

    }
    
    private IEnumerator lost()
    {
        yield return new WaitForSeconds(4f);
        gameHandlerScript.lose();
    
    }
    
    private IEnumerator won()
    {
        
        yield return new WaitForSeconds(4f);
        
        gameHandlerScript.win();
    }

    private void Start()
    {
        winorlosecam.transform.position = player.transform.position + Vector3.forward * 5f + Vector3.up * 1f;
        pm.DisableMoving();
        Target.SetActive(true);
        var position = Target2.transform.position;
        Target.transform.DOMove(new Vector3
            (position.x, position.y, position.z), 1f)
            .SetEase(Ease.InOutSine).SetLoops(-1,LoopType.Yoyo);
    }
    private void Shoot()
    {
        
        Vector3 shoot = (Target.transform.position - transform.position).normalized;
        GetComponent<Rigidbody>().AddForce(shoot * power,ForceMode.Impulse);
        StartCoroutine(wait());
    }
}
