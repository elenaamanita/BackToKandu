using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Transform target;
    public float lookSmooth = 0.09f;
    public Vector3 offsetFromTarget = new Vector3(0, 1, -4);
    public float xTilt = 10;

    Vector3 destination = Vector3.zero;
    CharacterController characterController;
    float rotateVelocity = 0;

    void Start()
    {
        SetCameraTarget(target);
    }

    // set camera a new target to look at
    void SetCameraTarget(Transform t)
    {
        target = t;

        if (target != null)
        {
            if (target.GetComponent<CharacterController>())
            {
                characterController = target.GetComponent<CharacterController>();
            }
            else
                Debug.LogError("The camera's target needs a character controller");
        }
        else
            Debug.LogError("Your camera needs a target");
    }

    void LateUpdate()
    {
        //moving
        MovetoTarget();
        //rotating
        LookAtTarget();
    }

    void MovetoTarget()
    {
        destination = characterController.TargetRotation * offsetFromTarget;
        destination += target.position;
        transform.position = destination;
    }

    void LookAtTarget()
    {
        float eulerYAngle = Mathf.SmoothDampAngle(  transform.localEulerAngles.y,
                                                    target.localEulerAngles.y,
                                                    ref rotateVelocity, 
                                                    lookSmooth);
        transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, eulerYAngle, 0);
    }

}
