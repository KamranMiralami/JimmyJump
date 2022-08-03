using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallImpactHandlerScript : MonoBehaviour
{
    [SerializeField] Animator modelAnimation;
    public AudioSource deathAudio;
    bool isDeathPlayed = false;
    public KnockOutBehaviour ko;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            if(ko!=null)
            {
                ko.Hit();
            }
            modelAnimation.SetBool("isDead", true);
            if (!isDeathPlayed)
            {
                isDeathPlayed = true;
                deathAudio.Play();
            }
            transform.parent.GetComponent<GuardBehaviour>().canMove = false;
            transform.parent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            StartCoroutine(resizeCollider());
        }
    }

    public IEnumerator resizeCollider()
    {
        float initialHeight = transform.parent.GetComponent<CapsuleCollider>().height;
        float initialRadius = transform.parent.GetComponent<CapsuleCollider>().radius;
        float t = 0f;
        while (t < 1.6)
        {
            t += Time.deltaTime;
            yield return null;
        }
        t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime;
            transform.parent.GetComponent<CapsuleCollider>().height = Mathf.Lerp(initialHeight, 0.2f, t);
            transform.parent.GetComponent<CapsuleCollider>().radius = Mathf.Lerp(initialRadius, 0.2f, t);
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
