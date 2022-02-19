using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Fields
    private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    private Vector3 moveDirection;
    private Vector3 velocity;

    [SerializeField] private GameObject VFX;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private GameObject effectPosition;

    private CharacterController characterController;
    #endregion

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        //isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        //if (isGrounded && velocity.y < 0)
        //{
        //    velocity.y = -2f;
        //}

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        moveDirection = new Vector3(horizontal, 0, vertical);

        VFXRotation();

        if (moveDirection != Vector3.zero)
        {
            // Walk
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                Walk();
            }
            // Run
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                Run();
            }
            // Idle
            else
            {
                Idle();
            }
        }

        moveDirection *= moveSpeed;

        characterController.Move(moveDirection * Time.deltaTime);

        //velocity.y += gravity * Time.deltaTime;
        //characterController.Move(velocity * Time.deltaTime);
    }
    
    private void VFXRotation()
    {
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            targetRotation = Quaternion.RotateTowards(VFX.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            VFX.transform.rotation = targetRotation;       
            effectPosition.transform.rotation = targetRotation;
        }            
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
    }

    private void Run()
    {
        moveSpeed = runSpeed;
    }

    private void Idle()
    {

    }
}
