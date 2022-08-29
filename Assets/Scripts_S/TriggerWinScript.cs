using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWinScript : MonoBehaviour
{
    [SerializeField] GameObject gameHandler;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.forward = new Vector3(-1, 0, 0);
            other.gameObject.GetComponent<PlayerMove>().vaultFunc(false);
            gameHandler.GetComponent<GameHandlerScript>().win();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
