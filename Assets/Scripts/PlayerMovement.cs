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

    [SerializeField] GameObject VFX;
    [SerializeField] float rotationSpeed;

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
        VFX.transform.rotation = Quaternion.LookRotation(moveDirection, Vector3.up);
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
