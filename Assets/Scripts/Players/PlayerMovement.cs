using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    #region Fields
    private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private LayerMask layerMask;
    private Vector3 target;
    private Vector3 offset;

    private Vector3 moveDirection;
    private Vector3 velocity;

    [SerializeField] private GameObject VFX;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private GameObject effectPosition;

    private CharacterController characterController;
    private NavMeshAgent agent;
    #endregion

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Move();
    }

    private void OnDisable()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        target = transform.position - Vector3.up;
    }

    private void OnEnable()
    {
        agent.isStopped = false;
    }

    private void Move()
    {
        //isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        //if (isGrounded && velocity.y < 0)
        //{
        //    velocity.y = -2f;
        //}

        //float vertical = Input.GetAxis("Vertical");
        //float horizontal = Input.GetAxis("Horizontal");

        //moveDirection = new Vector3(horizontal, 0, vertical);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                Debug.Log(hit.collider.gameObject.name);
                target = hit.point;
                //offset = target - transform.position;
                //moveDirection = new Vector3((float)target.x - transform.position.x, 0, (float)target.z - transform.position.z).normalized;
            }
        }
        //Debug.DrawRay(transform.position, transform.position + test - (Vector3.up), Color.yellow);
            
        VFXRotation();

        if (offset.magnitude > .01f)
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


        agent.SetDestination(target);
        Debug.Log(offset.magnitude);
        //if(offset.magnitude > .1f)
        //{
        //    //offset = target - transform.position;
        //    //moveDirection = moveSpeed * moveDirection;
        //    Debug.Log(moveDirection);
        //    characterController.Move(moveDirection * Time.deltaTime);
        //}
            

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
