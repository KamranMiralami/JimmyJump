using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompassScript : MonoBehaviour
{
    [SerializeField] GameObject objective;
    [SerializeField] GameObject player;
    [SerializeField] float scale = 1;
    [SerializeField] float distance = 1;
    [SerializeField] Color color = Color.yellow;
    [SerializeField] Material compassMat;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localScale = new Vector3(scale, scale, scale);
        compassMat.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Vector3.Normalize(objective.transform.position - player.transform.position) * distance;
        dir.y = 0;
        gameObject.transform.forward = dir;
        Vector3 pos = player.transform.position + dir;
        gameObject.transform.position = pos;
    }
}
