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
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        moveDirection = new Vector3(horizontal, 0, vertical);

        if(moveDirection != Vector3.zero)
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
