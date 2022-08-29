using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float vaultDuration = 1.8f;
    [SerializeField] float vaultDistance = 2f;
    [SerializeField] GameObject gameHandler;
    public Joystick joystick;
    public Animator anim;
    public CharacterController characterController;
    public float speed=5f;
    public CameraFollow cameraFollow;
    Vector3 cf;
    float cameraOffsetPercentage=0f;
    Vector3 direction = Vector3.zero;
    public bool moveEnable=true;
    float scaledSpeed;
    private void Start()
    {
        cf = new Vector3(cameraFollow.offset.x, cameraFollow.offset.y, cameraFollow.offset.z);
        vaultFunc(true);
    }

    public void vaultFunc(bool enableAfterVault)
    {
        anim.SetBool("isRunning", false);
        anim.SetBool("isVaulting", true);
        DisableMoving();
        gameHandler.GetComponent<GameHandlerScript>().disableCompass();
        StartCoroutine(vault(vaultDistance, vaultDuration, enableAfterVault));
    }

    IEnumerator vault(float distance, float duration, bool enableAfterVault)
    {
        float t = 0f;
        bool temp = false;
        Vector3 initial = gameObject.transform.position;
        Vector3 dest = (gameObject.transform.forward * distance) + initial;
        while (t < 1)
        {
            if(!temp && t >= 0.3)
            {
                temp = true;
                anim.SetBool("isVaulting", false);
            }
            t += Time.deltaTime / duration;
            gameObject.transform.position = Vector3.Lerp(initial, dest, t);
            yield return null;
        }
        if (enableAfterVault)
        {
            EnableMoving();
            gameHandler.GetComponent<GameHandlerScript>().enableCompassIfNeeded();
        }
    }
    private void FixedUpdate()
    {
        if(!moveEnable)
        {
            return;
        }
        direction = new Vector3(0, 0, 1) * joystick.Vertical + new Vector3(1, 0, 0) * joystick.Horizontal;
        scaledSpeed=Mathf.Lerp(0, speed, Mathf.Sqrt(direction.x * direction.x + direction.z * direction.z));
        anim.SetFloat("animSpeed", scaledSpeed/speed);
        //direction = -1* direction.normalized;
        if (direction == Vector3.zero)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", false);
            //direction=transform.forward;
            //cameraOffsetPercentage =Mathf.Clamp01(cameraOffsetPercentage- Time.deltaTime*0.1f);
            cameraOffsetPercentage = 0;
        }
        else
        {
            Debug.Log(scaledSpeed / speed);
            if (scaledSpeed / speed < 0.4f)
            {
                anim.SetBool("isWalking", true);
                StartCoroutine(RunDelay());
            }
            else
            {
                anim.SetBool("isRunning", true);
                StartCoroutine(WalkDelay());
            }
            cameraOffsetPercentage = Mathf.Clamp01(cameraOffsetPercentage + Time.deltaTime * 0.002f);
        }
        //Debug.LogWarning(cameraOffsetPercentage);
        transform.forward = Vector3.Lerp(transform.forward, direction, 15f * Time.deltaTime);
        characterController.SimpleMove(direction.normalized * scaledSpeed);
        cameraFollow.offset.x = Mathf.Lerp(cameraFollow.offset.x, cf.x + direction.x * 1.5f, cameraOffsetPercentage);
        cameraFollow.offset.z = Mathf.Lerp(cameraFollow.offset.z, cf.z + direction.z * 1.5f, cameraOffsetPercentage);
    }
    IEnumerator RunDelay()
    {
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("isRunning", false);
    }
    IEnumerator WalkDelay()
    {
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("isWalking", false);
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

    public void Death(Vector3 forward)
    {
        forward.y = 0;
        gameObject.transform.forward = forward;
        anim.SetBool("isDead", true);
        DisableMoving();
        StartCoroutine(playerDeathFall(1.5f, transform.position.y - (transform.localScale.y / 2) - 0.2f));
        gameHandler.GetComponent<GameHandlerScript>().lose();
    }

    IEnumerator playerDeathFall(float duration, float dest)
    {
        float t = 0f;
        Vector3 initialPos = gameObject.transform.position;
        while (t < 1)
        {
            t += Time.deltaTime / duration;
            gameObject.transform.position = new Vector3(
                initialPos.x,
                Mathf.Lerp(initialPos.y, dest, t),
                initialPos.z);
            yield return null;
        }
    }
}
