using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public float health;
    //Patroling
    public Vector3 walkPoint;
    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    private GameObject[] otherEnemys;
    public GameObject damageText;

    //States
    public float sightRange, attackRange, supportRange;
    public bool playerInSightRange, playerInAttackRange;
    private Animator animator;

    private void Awake()
    {
        otherEnemys = GameObject.FindGameObjectsWithTag("Enemy");
        animator=GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator.SetTrigger("IdleBreak2");
    }

    private void Update()
    {
        if (Mathf.Abs(transform.position.z - player.transform.position.z) <= sightRange)
            playerInSightRange = true;
        else
            playerInSightRange = false;

        if (Mathf.Abs(transform.position.z - player.transform.position.z) <= attackRange)
            playerInAttackRange = true;
        else
            playerInAttackRange = false;


        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
            CheckOtherEnemy();
        }

        if (playerInAttackRange && playerInSightRange) AttackPlayer();

        if(Input.GetMouseButtonDown(1))
        {
            DamageIndicator indicator=Instantiate(damageText,transform.position,Quaternion.identity).GetComponent<DamageIndicator>();
            indicator.SetDamageText(Random.Range(10,30));
        }
    }
    public void ChasePlayer()
    {
        agent.SetDestination(player.position);
        if(!this.animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            animator.SetTrigger("Walk");
        }
    }
    void CheckOtherEnemy()
    {
        for (int i = 0; i < otherEnemys.Length; i++)
        {
            if (otherEnemys[i].gameObject != this.gameObject && Mathf.Abs(transform.position.z - otherEnemys[i].transform.position.z) <= supportRange)
            {
                otherEnemys[i].GetComponent<EnemyAI>().ChasePlayer();
            }
        }
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);
        Vector3 targetPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(targetPos);
        if (!alreadyAttacked)
        {
            ///Attack code here
            if(!this.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1") || !this.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2") || !this.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3") || !this.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack4"))
            {
                int randomAttack=Random.Range(1,4);
                animator.SetTrigger("Attack"+randomAttack);
            }
            
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, supportRange);
    }
}
