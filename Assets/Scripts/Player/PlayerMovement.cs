using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumVelocity;
    [SerializeField] private float speedM;
    [SerializeField] private float runTime;
    private Vector3 movmentVector;
    private Vector3 cameraMovement;
    private Vector3 velocity;
    private Rigidbody feet;
    private int jump = 1;
    private int maxJump;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float currentRunTime;
    private bool upRunTime;
    private bool downRunTime;




    void Start ()
    {
        velocity = new Vector3(0,jumVelocity,0);
        maxJump = 1;
        currentSpeed = walkSpeed;
        feet = GetComponent<Rigidbody>();
        currentRunTime = runTime;
	}

	void Update ()
    {
        movmentVector.x = Input.GetAxis("Horizontal");
        movmentVector.z = Input.GetAxis("Vertical");
        transform.Translate(movmentVector * currentSpeed * Time.deltaTime);

        cameraMovement.y = Input.GetAxis("Mouse X");
        transform.Rotate(cameraMovement *speedM);

        if (upRunTime)
        {
            currentRunTime += Time.deltaTime;
        }
        if (currentRunTime >= runTime)
        {
            currentRunTime = runTime;
        }
        if (currentRunTime <= 0)
        {
            currentSpeed = walkSpeed;
            currentRunTime = 0;
            downRunTime = false;
        }
        if (currentRunTime > 0)
        {
            downRunTime = true;
        }

        if (Input.GetButton("LeftShift"))
        {
            if (downRunTime)
            {
                currentSpeed = runSpeed;
                currentRunTime -= Time.deltaTime;
            }
            upRunTime = false;
        }
        if (Input.GetButtonUp("LeftShift"))
        {
            currentSpeed = walkSpeed;
            upRunTime = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (jump < maxJump)
            {
                feet.velocity = velocity;
                jump += 1;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        jump = 0;
    }
}
