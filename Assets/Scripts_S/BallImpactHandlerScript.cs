using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallImpactHandlerScript : MonoBehaviour
{
    [SerializeField] Animator modelAnimation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            modelAnimation.SetBool("isDead", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
