using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies;
using UnityEngine;
using UnityEngine.Rendering;

public class FPSController : MonoBehaviour
{
    public bool CanMove { get; private set; } = true;
    private bool IsSprinting => Input.GetButton("Sprint");
    private bool CanJump => Input.GetButtonDown("Jump")&&characterController.isGrounded;
    //move
    [SerializeField] private float walkSpeed = 3.0f;

    [SerializeField] private float sprintSpeed = 4.5f;


    //look
    [SerializeField, Range(1, 10)] private float XlookSpeed = 2.0f;
    [SerializeField, Range(1, 10)] private float ylookSpeed = 2.0f;
    [SerializeField, Range(1, 180)] private float upperLookLimit = 80.0f;
    [SerializeField, Range(1, 180)] private float lowerLookLimit = 80.0f;

    [SerializeField] private float jumpStrength = 9.0f;

    [SerializeField] private float gravity = 30.0f;

    private Camera cam;
    private CharacterController characterController;

    private Vector3 moveDirection;
    private Vector2 cInput;

    private float rotationX;



    void Awake()
    {
        cam = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove)
        {
            MoveInput();
            MouseLook();
            FinalMove();
            Jump();
        }
    }
    private void MoveInput()
    {
        cInput = new Vector2((IsSprinting?sprintSpeed: walkSpeed) * Input.GetAxis("Vertical"), (IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Horizontal"));

        float moveDirectionY=moveDirection.y;
        moveDirection=(transform.TransformDirection(Vector3.forward)*cInput.x)+(transform.TransformDirection(Vector3.right)*cInput.y);
        moveDirection.y = moveDirectionY;

    }
    private void Jump()
    {
        if (CanJump)
            moveDirection.y = jumpStrength;
    }
    private void MouseLook()
    {
        rotationX -=Input.GetAxis("Mouse Y")*ylookSpeed;
        rotationX = Mathf.Clamp(rotationX,-upperLookLimit,lowerLookLimit);
        cam.transform.localRotation=Quaternion.Euler(rotationX,0,0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * XlookSpeed, 0);
    }
    private void FinalMove()
    {
        if(!characterController.isGrounded)
            moveDirection.y-=gravity*Time.deltaTime;
        characterController.Move(moveDirection*Time.deltaTime);
    }
}
