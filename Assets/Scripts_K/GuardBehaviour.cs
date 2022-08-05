using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GuardBehaviour : MonoBehaviour
{
    public GameObject chasingGO = null;
    public float speed;
    public Animator modelAnimation;
    public GameObject range;
    public AudioSource runAudio;
    bool isPlayingRun = false;
    public bool canMove = true;
    private void OnTriggerEnter(Collider other)
    {
        if(chasingGO == null)
            if(other.gameObject.CompareTag("Player"))
            {
                range.SetActive(false);
                modelAnimation.SetBool("isRunning", true);
                if (!isPlayingRun)
                {
                    runAudio.Play();
                    isPlayingRun = true;
                }
                //Debug.Log("player in " + gameObject.name + " range ");
                chasingGO = other.gameObject;
                //GetComponentInChildren<RangeRender>().DrawCircle();
            }
    }
    private void Start()
    {
        if (chasingGO != null)
        {
            range.SetActive(false);
            modelAnimation.SetBool("isRunning", true);
            if (!isPlayingRun)
            {
                runAudio.Play();
                isPlayingRun = true;
            }
        }
    }
    private void Update()
    {
        if(chasingGO != null && canMove)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                chasingGO.transform.position + new Vector3(0, transform.localScale.y,0),
                speed * Time.deltaTime
                );
            transform.forward = chasingGO.transform.position - transform.position;
        }
    }

    public void stop()
    {
        canMove = false;
        modelAnimation.SetBool("isRunning", false);
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        Quaternion temp = gameObject.transform.rotation;
        runAudio.Stop();
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, temp.eulerAngles.y, temp.eulerAngles.z));
    }
}
