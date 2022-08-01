using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GuardBehaviour : MonoBehaviour
{
    public GameObject chasingGO = null;
    public float speed;
    public Animator modelAnimation;
    public GameObject range;
    public AudioSource audioSource;
    bool isPlaying = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            range.SetActive(false);
            modelAnimation.SetBool("isRunning", true);
            if (!isPlaying)
            {
                audioSource.Play();
                isPlaying = true;
            }
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
