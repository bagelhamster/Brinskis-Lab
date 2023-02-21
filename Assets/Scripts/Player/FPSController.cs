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
    private bool CanCrouch=>Input.GetButtonDown("Crouch")&&!duringanimation&&characterController.isGrounded;
    //move
    [SerializeField] private float walkSpeed = 3.0f;    [SerializeField] private float walkSpeed2 = 3.0f;
    [SerializeField] private float crouchSpeed = 1.5f;
    [SerializeField] private float sprintSpeed = 4.5f;
    [SerializeField] private float slopeSpeed = 9f;
    [SerializeField] private float WallRunSpeed = 7f;
    //crouch
    [SerializeField] private float crouchH = 0.5f;
    [SerializeField] private float standingH = 2f;
    [SerializeField] private float timeToCrouch = 0.25f;
    [SerializeField] private Vector3 centercrouch = new Vector3(0,0.5f,0);
    [SerializeField] private Vector3 standcenter=new Vector3(0,0,0);
    private bool isCrouching;
    private bool duringanimation;
    
    //look
    [SerializeField, Range(1, 10)] private float XlookSpeed = 2.0f;
    [SerializeField, Range(1, 10)] private float ylookSpeed = 2.0f;
    [SerializeField, Range(1, 180)] private float upperLookLimit = 80.0f;
    [SerializeField, Range(1, 180)] private float lowerLookLimit = 80.0f;

    [SerializeField] private float jumpStrength = 9.0f;

    [SerializeField] private float gravity = 30.0f;
    [SerializeField] private float gravity2 = 30.0f;    [SerializeField] private float gravity3 = 5.0f;

    //bob
    [SerializeField] private float walkbob = 14f;
    [SerializeField] private float walkbobamount = 0.04f;
    [SerializeField] private float sprintbob = 18f;
    [SerializeField] private float sprintbobamount = 0.1f;
    [SerializeField] private float crouchbob = 8f;
    [SerializeField] private float crouchbobamount = 0.01f;
    private float defaulty = 0;
    private float timer;
    List<Collider> collidedObjects = new List<Collider>();
    private int collided;
    private Vector3 hitPoint;
    private bool IsSliding
    {
        get
        {
            if(characterController.isGrounded&&Physics.Raycast(transform.position,Vector3.down,out RaycastHit slopeHit, 2f))
            {
                hitPoint = slopeHit.normal;
                return Vector3.Angle(hitPoint, Vector3.up) > characterController.slopeLimit;
            }
            else
            {
                return false;
            }
        }
    }

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
        defaulty = cam.transform.localPosition.y;

    }

    void Update()
    {
        if (CanMove)
        {
            MoveInput();
            MouseLook();
            FinalMove();
            Jump();
            Crouch();
            HeadBob();
            touching();
        }
    }
    private void FixedUpdate()
    {
        collidedObjects.Clear();
    }
    
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        collidedObjects.Add(hit.collider);

        if (hit.gameObject.tag == ("Wall"))
        {
            gravity = gravity3;
            walkSpeed = WallRunSpeed;
            

        }
        else
        {
            gravity = gravity2;
            walkSpeed = walkSpeed2;

        }
    }

    private void MoveInput()
    {
        cInput = new Vector2((isCrouching?crouchSpeed:IsSprinting?sprintSpeed: walkSpeed) * Input.GetAxis("Vertical"), (isCrouching ? crouchSpeed : IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Horizontal"));

        float moveDirectionY=moveDirection.y;
        moveDirection=(transform.TransformDirection(Vector3.forward)*cInput.x)+(transform.TransformDirection(Vector3.right)*cInput.y);
        moveDirection.y = moveDirectionY;

    }
    private void Jump()
    {
        if (CanJump)
            moveDirection.y = jumpStrength;
    }
    private void HeadBob()
    {
        if (!characterController.isGrounded) return;

        if (Mathf.Abs(moveDirection.x) > 0.1 || Mathf.Abs(moveDirection.z) > 0.1)
        {
            timer += Time.deltaTime * (isCrouching ? crouchbob : IsSprinting ? sprintbob : walkbob);
            cam.transform.localPosition=new Vector3(cam.transform.localPosition.x,defaulty+Mathf.Sin(timer)*(isCrouching?crouchbobamount:IsSprinting?sprintbobamount:walkbobamount),cam.transform.localPosition.z);
        }
    }
    private void Crouch()
    {
        if (CanCrouch)
            StartCoroutine(CrouchStand());
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
        if (IsSliding)
            moveDirection += new Vector3(hitPoint.x, -hitPoint.y, hitPoint.z) * slopeSpeed;
        characterController.Move(moveDirection*Time.deltaTime);

    }
    private void touching()
    {
        collided=collidedObjects.Count;
        if(collided==0&&gravity!=gravity2)
        {
            gravity = gravity2;
            walkSpeed = walkSpeed2;
        }
        //Debug.Log(collided);

        
    }
    private IEnumerator CrouchStand()
    {
        if (isCrouching && Physics.Raycast(cam.transform.position, Vector3.up, 1f))
        yield break;

        duringanimation = true;
        float timeElapsed = 0;
        float targetHeight=isCrouching ? standingH:crouchH;
        float currentHeight = characterController.height;
        Vector3 targetCenter = isCrouching ? standcenter : centercrouch;
        Vector3 currentCenter = characterController.center;
        while(timeElapsed<timeToCrouch)
        {
            characterController.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCrouch);
            characterController.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        characterController.height = targetHeight;
        characterController.center = targetCenter;
        isCrouching = !isCrouching;

        duringanimation = false;
    }
}
