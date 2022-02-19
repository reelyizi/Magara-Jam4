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

    //States
    public float sightRange, attackRange, supportRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        otherEnemys = GameObject.FindGameObjectsWithTag("Enemy");
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
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
    }
    public void ChasePlayer()
    {
        agent.SetDestination(player.position);

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
