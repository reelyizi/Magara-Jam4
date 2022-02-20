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
    public LayerMask whatIsPlayer;

    //States
    public float sightRange, attackRange, supportRange;
    public bool playerInSightRange, playerInAttackRange;
    private Animator animator;
    public enum LifeState { lives, death };
    public LifeState lifeState;


    private void Awake()
    {
        lifeState = LifeState.lives;
        otherEnemys = GameObject.FindGameObjectsWithTag("Enemy");
        animator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator.SetTrigger("IdleBreak2");
    }

    private void Update()
    {
        if (lifeState == LifeState.lives)
        {
            /*if (Mathf.Abs(transform.position.z - player.transform.position.z) <= sightRange)
                playerInSightRange = true;
            else
                playerInSightRange = false;

            if (Mathf.Abs(transform.position.z - player.transform.position.z) <= attackRange)
                playerInAttackRange = true;
            else
                playerInAttackRange = false;*/

            //Check for sight and attack range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);



            if (playerInSightRange && !playerInAttackRange)
            {
                //Move
                ChasePlayer();
                CheckOtherEnemy();
            }
            if (playerInAttackRange && playerInSightRange)
            {
                //Attack
                AttackPlayer();
            }

            if (Input.GetMouseButtonDown(1))
            {
                DamageIndicator indicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
                indicator.SetDamageText(Random.Range(10, 30));
                TakeDamage(40);
            }
        }
        if (lifeState == LifeState.death)
        {
            agent.enabled = false;
        }
    }
    public void ChasePlayer()
    {
        agent.SetDestination(player.position);
        if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName("Walk") && !this.animator.GetCurrentAnimatorStateInfo(0).IsName("GotHit") && !playerInAttackRange)
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
            if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1") || !this.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
            {
                int randomAttack = Random.Range(1, 3);
                animator.SetTrigger("Attack" + randomAttack);
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


        if (health <= 0) DestroyEnemy();
        else TakeDamageEffect();
    }
    private void TakeDamageEffect()
    {
        lifeState = LifeState.death;
        animator.SetTrigger("GotHit");
        Invoke("ReWalk", 1.2f);
    }
    void ReWalk()
    {
        lifeState = LifeState.lives;
        agent.enabled = true;
    }
    private void DestroyEnemy()
    {
        lifeState = LifeState.death;
        animator.SetTrigger("Death");
        Destroy(gameObject, 3);
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
