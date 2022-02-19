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

    private void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Walkable"))
                    target = hit.point;

                Debug.Log(hit.collider.gameObject.name);
                offset = target - transform.position;
                moveDirection = new Vector3((float)target.x - transform.position.x, 0, (float)target.z - transform.position.z).normalized;
            }
        }
        //Debug.DrawRay(transform.position, transform.position + test - (Vector3.up), Color.yellow);

        VFXRotation();

        // Walk
        if (offset.magnitude > .1f)
        {
            animator.SetBool("Movement", true);
            Walk();
        }
        // Idle
        else
        {
            animator.SetBool("Movement", false);
            Idle();
        }

        Debug.Log(offset.magnitude);
        if (offset.magnitude > .1f)
        {
            Debug.Log(offset.magnitude);
            characterController.Move((moveDirection * moveSpeed) * Time.deltaTime);
            offset = target - transform.position;
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
        moveSpeed = walkSpeed;
    }

    private void Idle()
    {

    }
}
