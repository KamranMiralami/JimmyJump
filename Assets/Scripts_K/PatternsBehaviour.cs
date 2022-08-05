using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternsBehaviour : MonoBehaviour
{
    public bool triggered = false;
    public Color32 startColor;
    public Color32 endColor;
    Renderer renderer;
    PatternHandler handler;
    private void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.material.SetColor("_Color", startColor);
        handler = GetComponentInParent<PatternHandler>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (gameObject == handler.patterns[handler.num])
            {
                renderer.material.SetColor("_Color", endColor);
                triggered = true;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!triggered)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (gameObject == handler.patterns[handler.num])
                {
                    renderer.material.SetColor("_Color", endColor);
                    triggered = true;
                }
            }
        }
    }
}
