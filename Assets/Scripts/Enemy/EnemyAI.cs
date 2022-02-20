using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public float health,maxHealth;
    //Patroling
    public Vector3 walkPoint;
    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject healthUI;
    private GameObject[] otherEnemys;
    public GameObject damageText;
    public LayerMask whatIsPlayer;
    [SerializeField] private Image healthImage;

    //States
    public float sightRange, attackRange, supportRange;
    public bool playerInSightRange, playerInAttackRange;
    private Animator animator;
    public enum LifeState { lives, death };
    public LifeState lifeState;


    private void Awake()
    {
        maxHealth=health;
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


            CheckVisible();
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
                TakeDamage(100);
            }
        }
        if (lifeState == LifeState.death)
        {
            agent.enabled = false;
        }
    }
    private void CheckVisible()
    {
        Ray rayToCameraPos = new Ray(transform.position, Camera.main.transform.position-transform.position);
        var dir = gameObject.transform.position - Camera.main.transform.position;
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position,dir,out hit))
        {
            Debug.DrawLine(Camera.main.gameObject.transform.position,hit.point,Color.red);
            Debug.Log(hit.collider.name+", "+hit.collider.tag);
            if(hit.collider.tag!="Enemy")
            {
                this.gameObject.GetComponent<EPOOutline.Outlinable>().enabled=true;
            }
            else
            {
                this.gameObject.GetComponent<EPOOutline.Outlinable>().enabled=false;
            }
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
        this.gameObject.GetComponent<EPOOutline.Outlinable>().enabled=true;
        Invoke("DeActiveOutlineable",0.2f);
        if (health <= 0)
        {
            DestroyEnemy();
            Destroy(healthUI);
        } 
        else 
        {
            healthImage.fillAmount=health/maxHealth;
            TakeDamageEffect();
        }
    }
    private void DeActiveOutlineable()
    {
        this.gameObject.GetComponent<EPOOutline.Outlinable>().enabled=false;
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
