using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallImpactHandlerScript : MonoBehaviour
{
    [SerializeField] Animator modelAnimation;
    [SerializeField] float ballMinSpeed = 3f;
    public AudioSource deathAudio;
    public bool isDeathPlayed = false;
    public KnockOutBehaviour ko;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball") && !isDeathPlayed)
        {
            if (other.gameObject.GetComponent<Rigidbody>().velocity.magnitude >= ballMinSpeed)
            {
                if (ko != null)
                {
                    ko.Hit();
                }
                modelAnimation.SetBool("isDead", true);
                if (!isDeathPlayed)
                {
                    isDeathPlayed = true;
                    deathAudio.Play();
                }
                transform.parent.GetComponent<GuardBehaviour>().stop();
                StartCoroutine(resizeCollider());
            }
        }
        else if (other.gameObject.CompareTag("Player") && !isDeathPlayed)
        {
            if (transform.parent.GetComponent<GuardBehaviour>().canMove)
            {
                transform.parent.gameObject.GetComponent<GuardBehaviour>().stop();
                modelAnimation.SetBool("isPunching", true);
                other.gameObject.GetComponent<PlayerMove>().Death(
                    gameObject.transform.position - other.gameObject.transform.position);
                StartCoroutine(delayedFalse());
            }
        }
    }

    IEnumerator delayedFalse()
    {
        yield return new WaitForSeconds(0.5f);
        modelAnimation.SetBool("isPunching", false);
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
