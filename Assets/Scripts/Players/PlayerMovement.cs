using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    #region Fields
    private float moveSpeed;
    [SerializeField] private float walkSpeed;
    private Vector3 target;
    private Vector3 offset;

    private Vector3 moveDirection;

    [SerializeField] private GameObject VFX;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private GameObject effectPosition;

    [SerializeField] private Animator animator;

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

    private void OnDisable()
    {
        moveDirection = Vector3.zero;
        offset = Vector3.zero;
    }

    private void Move()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Walkable"))
                    target = hit.point;

                offset = target - transform.position;
                moveDirection = new Vector3((float)target.x - transform.position.x, 0, (float)target.z - transform.position.z).normalized;
            }
        }
        //Debug.DrawRay(transform.position, transform.position + test - (Vector3.up), Color.yellow);

        VFXRotation();

        // Walk
        if (offset.magnitude > .5f)
        {           
            Walk();

            characterController.Move((moveDirection * moveSpeed) * Time.deltaTime);
            transform.position = transform.position - (Vector3.up * transform.position.y);
            offset = target - transform.position;
        }
        // Idle
        else
        {
            Idle();
        }
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
        animator.SetBool("Movement", true);
        moveSpeed = walkSpeed;
    }

    private void Idle()
    {
        animator.SetBool("Movement", false);
    }
}
