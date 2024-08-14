using Cinemachine.Utility;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IMovementReadable, IVisible
{
    [Header("Movement info")]
    [SerializeField] float speedWalking = 1.5f;
    [SerializeField] float speedRunning = 3f;
    [SerializeField] float jumpSpeed = 6f;

    public enum MovementMode
    {
        RelativeToCamera,
        Local
    }
    [SerializeField] MovementMode movementMode = MovementMode.Local;
    [SerializeField] Transform movementCamera;

    [Header("Camera info")]
    [SerializeField] Transform cameraTransform;

    [Header("Movement Variables")]
    private Vector2 inputMovement;
    private Vector3 localVelocity;
    private Vector3 planeVelocity;
    private Vector3 lastVelocity;


    [Header("Orientation Variables")]
    [SerializeField] private float angularSpeed = 360;
    public enum OrientationMode
    {
        MovementForward,
        CameraForward,
        LookAtTarget
    }
    [SerializeField] private OrientationMode orientationMode;
    [SerializeField] private Transform orientationCamera;
    [SerializeField] private Transform orientationTarget;

    [Header("AI Visibility")]
    [SerializeField] private Transform visibilitCheckPointsParent;
    private Transform[] visibilityCheckpoints;

    private bool inputJump = false;
    private bool inputRun;

    private CharacterController characterController;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();

        visibilityCheckpoints = new Transform[visibilitCheckPointsParent.childCount];
        for (int i = 0; i < visibilityCheckpoints.Length; i++)
        { visibilityCheckpoints[i] = visibilitCheckPointsParent.GetChild(i).transform; }
    }

    private void Update()
    {
        UpdateMovementOnPlane();
        UpdateMovementOnVertical();
        UpdateRotation();
    }

    #region Update Movement

    private void UpdateMovementOnPlane()
    {
        CalculateLocalVelocity();
        CalculatePlaneVelocity();
        MovePlayer();
    }
    private void CalculateLocalVelocity()
    {
        float speed = inputRun ? speedRunning : speedWalking;

        localVelocity.x = inputMovement.x;
        localVelocity.z = inputMovement.y;
        localVelocity = localVelocity * speed;
    }
    private void CalculatePlaneVelocity()
    {
        switch (movementMode)
        {
            case MovementMode.RelativeToCamera:
                planeVelocity = movementCamera.TransformDirection(localVelocity);
            break;
            case MovementMode.Local:
                planeVelocity = transform.TransformDirection(localVelocity);
                break;
        }
        planeVelocity = Vector3.ProjectOnPlane(planeVelocity, Vector3.up);
        planeVelocity = planeVelocity.normalized * localVelocity.magnitude;

    }
    float currentVerticalSpeed = 0;
    float gravity = -9.8f;
    private void UpdateMovementOnVertical()
    {
        if (characterController.isGrounded)
            currentVerticalSpeed = 0f;

        if (inputJump && characterController.isGrounded)
            currentVerticalSpeed = jumpSpeed;
        else
            currentVerticalSpeed += gravity * Time.deltaTime;
        
        inputJump = false;
    }
    private void MovePlayer()
    {
        Vector3 combinedVelocity = planeVelocity + (currentVerticalSpeed * Vector3.up);
        characterController.Move(combinedVelocity * Time.deltaTime);
        lastVelocity = combinedVelocity;
    }
    #endregion

    #region Update Rotation
    
    private void UpdateRotation()
    {
        if (planeVelocity.sqrMagnitude > 0f)
        {
            Vector3 desiredForward = Vector3.zero;
            switch (orientationMode)
            {
                case OrientationMode.CameraForward:
                    desiredForward = planeVelocity.normalized;
                    break;
                case OrientationMode.MovementForward:
                    desiredForward = Vector3.ProjectOnPlane(orientationCamera.forward, Vector3.up);
                    break;
                case OrientationMode.LookAtTarget:
                    desiredForward = Vector3.ProjectOnPlane(orientationTarget.position - transform.position, Vector3.up);
                    break;
            }
            
            float angularDistanceWithSign = Vector3.SignedAngle(transform.forward, desiredForward, Vector3.up);
            float angularDistanceWithoutSign = Mathf.Abs(angularDistanceWithSign);

            float angleToApply = angularSpeed * Time.deltaTime;
            angleToApply = Mathf.Min(angularDistanceWithoutSign, angleToApply);
            angleToApply *= Mathf.Sign(angularDistanceWithSign);

            Quaternion rotationToApply = Quaternion.AngleAxis(angleToApply, Vector3.up);
            transform.rotation = rotationToApply * transform.rotation;
        }
    }

    #endregion

    #region Input Listeners Methods
    void OnJump()
    {
        inputJump = true;
    }

    void OnMove(InputValue value)
    {
        inputMovement = value.Get<Vector2>();
    }

    void OnRun(InputValue value)
    {
        inputRun = value.Get<float>() > 0;
    }

    #endregion

    #region IMovementReadable implementation
    Vector3 IMovementReadable.GetVelocity() { return lastVelocity; }
    float IMovementReadable.GetWalkSpeed() { return speedWalking; }
    float IMovementReadable.GetRunSpeed() { return speedRunning; }
    float IMovementReadable.GetJumpSpeed() { return jumpSpeed; }
    public bool GetGrounded() { return characterController.isGrounded; }
    public Vector3[] GetCheckPoints() 
    {
        Vector3[] checkPoints = new Vector3[visibilitCheckPointsParent.childCount];
        for (int i = 0; i < checkPoints.Length; i++)
            { checkPoints[i] = visibilityCheckpoints[i].position; }

        return checkPoints; 
    }
    public Transform GetTransform() { return transform; }

    #endregion
}
