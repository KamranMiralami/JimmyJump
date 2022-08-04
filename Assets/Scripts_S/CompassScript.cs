using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompassScript : MonoBehaviour
{
    [SerializeField] GameObject objective;
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = objective.transform.position - player.transform.position;
        dir.y = 0;
        float angle = Vector3.SignedAngle(dir, Vector3.forward, Vector3.up);
        gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
