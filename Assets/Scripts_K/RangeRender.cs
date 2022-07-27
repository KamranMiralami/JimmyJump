using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class RangeRender : MonoBehaviour
{
    public bool looping;
    public int CircleSteps;
    public int CircleDuration;
    LineRenderer circleRenderer;
    SphereCollider col;
    private void Start()
    {
        circleRenderer = GetComponent<LineRenderer>();
        col = GetComponentInParent<SphereCollider>();
        DrawRange();
    }
    async void DrawRange()
    {
        while (true)
        {
            for (float i = 0; i < col.radius; i+=0.1f)
            {
                await DrawCircle(CircleSteps, i, CircleDuration);
                if (!looping)
                    break;
            }
            if (!looping)
                break;
            await DrawCircle(CircleSteps, col.radius, CircleDuration+1000);
        }
    }
    async Task DrawCircle(int steps,float radius,int delay)
    {
        await Task.Delay(delay);
        circleRenderer.positionCount = steps;
        for(int currentStep=0;currentStep<steps; currentStep++)
        {
            float circumferenceProgress=(float)currentStep/steps;
            float currentRadian = circumferenceProgress * 2 * Mathf.PI;
            float xScale=Mathf.Cos(currentRadian);
            float yScale=Mathf.Sin(currentRadian);
            float x=xScale*radius;
            float y=yScale*radius;
            Vector3 currentPosition = new Vector3(x,-transform.parent.transform.position.y+0.1f, y)+transform.position;
            circleRenderer.SetPosition(currentStep, currentPosition);
        }
    }
    public async void DrawCircle()
    {
        looping = false;
        await Task.Delay(100);
        for (int currentStep = 0; currentStep < CircleSteps; currentStep++)
        {
            circleRenderer.SetPosition(currentStep, Vector3.zero);
        }
    }
}
