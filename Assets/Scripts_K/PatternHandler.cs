using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternHandler : MonoBehaviour
{
    [SerializeField] public GameObject[] patterns;
    [SerializeField] private PlayerMove player;
    [SerializeField] private GameHandlerScript gh;
    [SerializeField] private Animator Dance;
    [SerializeField] private GameObject target;
    private bool enable = true;
    [SerializeField] private GameObject[] guards;
    public int num = 0;
    public bool finished;
    void Start()
    {
        guards = GameObject.FindGameObjectsWithTag("Guard");
    }

    // Update is called once per frame
    void Update()
    {
        if (finished)
        {
            player.DisableMoving();
            var transform1 = player.transform;
            target.transform.position = transform1.position + Vector3.forward * 5f + Vector3.up * 1f;
            gh.focusCamera(target.transform.position,target.transform.rotation);
            Dance.SetBool("isDancing", true);
            StartCoroutine(won());
            finished = false;
        }
        if (patterns[num].GetComponent<PatternsBehaviour>().triggered == true)
        {
            if (enable)
            {
                num++;
                if (num == patterns.Length )

                {
                    for (int i = 0; i < guards.Length; i++)
                    {
                        guards[i].GetComponent<GuardBehaviour>().stop();
                        
                    }
                    
                    enable = false;
                    finished = true;
                }
            }
        }
    }

    private IEnumerator won()
    {
        
        yield return new WaitForSeconds(4f);
        
        gh.win();
    }
}
