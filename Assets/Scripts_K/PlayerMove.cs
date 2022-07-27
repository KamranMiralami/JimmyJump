using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public FixedJoystick joystick;
    public Animator anim;
    public CharacterController characterController;
    public float speed=5f;
    Vector3 direction = Vector3.zero;
    private void FixedUpdate()
    {
        direction = new Vector3(0, 0, 1) * joystick.Vertical + new Vector3(1, 0, 0) * joystick.Horizontal;
        direction = -1* direction.normalized;
        if (direction != Vector3.zero)
        {
            anim.SetBool("isRunning", true);
            transform.forward = Vector3.Lerp(transform.forward, direction, 150f * Time.deltaTime);
            characterController.SimpleMove(direction.normalized * speed);
        }
        else
        {
            anim.SetBool("isRunning", false);
            characterController.SimpleMove(direction.normalized * speed); // for later
        }
    }
}
