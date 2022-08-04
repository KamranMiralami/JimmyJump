using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public FixedJoystick joystick;
    public Animator anim;
    public CharacterController characterController;
    public float speed=5f;
    public CameraFollow cameraFollow;
    Vector3 cf;
    float cameraOffsetPercentage=0f;
    Vector3 direction = Vector3.zero;
    public bool moveEnable=true;
    private void Start()
    {
        cf = new Vector3(cameraFollow.offset.x, cameraFollow.offset.y, cameraFollow.offset.z);
        anim.SetBool("isRunning", true);
    }
    private void FixedUpdate()
    {
        if(!moveEnable)
        {
            return;
        }
        direction = new Vector3(0, 0, 1) * joystick.Vertical + new Vector3(1, 0, 0) * joystick.Horizontal;
        //direction = -1* direction.normalized;
        if (direction == Vector3.zero)
        {
            direction=transform.forward;
           //cameraOffsetPercentage =Mathf.Clamp01(cameraOffsetPercentage- Time.deltaTime*0.1f);
            cameraOffsetPercentage = 0;
        }
        else
        {
            cameraOffsetPercentage = Mathf.Clamp01(cameraOffsetPercentage + Time.deltaTime * 0.025f);
        }
        //Debug.LogWarning(cameraOffsetPercentage);
        transform.forward = Vector3.Lerp(transform.forward, direction, 150f * Time.deltaTime);
        characterController.SimpleMove(direction.normalized * speed);
        cameraFollow.offset.x = Mathf.Lerp(cameraFollow.offset.x, cf.x + direction.x * 3, cameraOffsetPercentage);
        cameraFollow.offset.z = Mathf.Lerp(cameraFollow.offset.z, cf.z + direction.z * 3, cameraOffsetPercentage);
    }
    public void EnableMoving()
    {
        //Debug.LogWarning("enabling");
        moveEnable = true;
        anim.SetBool("isRunning", true);
    }
    public void DisableMoving()
    {
        //Debug.LogWarning("disabling");
        moveEnable = false;
        anim.SetBool("isRunning", false);
    }
}
