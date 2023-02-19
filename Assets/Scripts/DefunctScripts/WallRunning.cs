using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    public LayerMask isWall;
    public LayerMask isGround;
    public float RunForce;
    public float MaxRunTime;
    public float RunTimer;

    public float horizontalInput;
    public float verticalInput;

    public float wallDistance;
    public float minJumpHeight;
    private RaycastHit leftWallHit;
    private RaycastHit rightWallHit;
    private bool LeftWall;
    private bool RightWall;

    private FPSController pm;
    public Transform orientation;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm=GetComponent<FPSController>();
    }
    private void Update()
    {
        CheckforWall();
    }
    private void CheckforWall()
    {
        RightWall = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallDistance, isWall);
        LeftWall = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallDistance, isWall);
    }
    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, isGround);
    }
    private void StateMachine()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if ((LeftWall || RightWall) && verticalInput > 0 && AboveGround())
        {
            if(!pm.wallrunning)
                WallRunStart();
        }
        else
        {
            if(pm.wallrunning)
                StopWallRun();
        }

    }
    private void WallRunStart()
    {
        pm.wallrunning = true;
    }
    private void WallRunnin()
    {
        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        Vector3 wallNormal = RightWall ? rightWallHit.normal : leftWallHit.normal;

        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
            wallForward = -wallForward;

        // push to wall force
        if (!(LeftWall && horizontalInput > 0) && !(RightWall && horizontalInput < 0))
            rb.AddForce(-wallNormal * 100, ForceMode.Force);
    }
    private void StopWallRun()
    {
        pm.wallrunning = false;
    }

}
