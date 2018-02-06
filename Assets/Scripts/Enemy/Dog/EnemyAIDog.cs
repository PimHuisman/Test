using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIDog : MonoBehaviour
{
    // NavMeshAgent
    NavMeshAgent agent;
    private Transform player;
    // RagDoll
    [SerializeField] private GameObject[] body;
    [SerializeField] private GameObject ragDoll;
    // isAttaking
    private RaycastHit hit;
    [SerializeField] private Transform head;
    [SerializeField] private float attackLength;
    [SerializeField] private bool attacking;
    [SerializeField] private float damage;
    [SerializeField] private float attackAgain;
    private float attackTime;
    // isChasing
    [SerializeField] private bool chasing;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    // isThinking
    [SerializeField] private float thinkTimer;
    [SerializeField] private float maxTimer;
    [SerializeField] private float walkTimer;
    [SerializeField] private float maxWalkTimer;
    private bool thinking;
    private bool walking;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        attackTime = attackAgain;
        walkTimer = maxWalkTimer;
        thinkTimer = maxTimer;
        walking = true;
        thinking = false;
    }
    void Update()
    {

        iswalking();
        RagDoll();
        isAttacking();
        isthinking();
        isChecking();
    }
    void isChecking()
    {
        // Attacking ??
        if (attacking)
        {
            attackTime -= Time.deltaTime;
        }
        if (attackTime <= 0)
        {
            attacking = false;
            attackTime = attackAgain;
        }
    }

    void RagDoll()
    {
        MeshCollider realenemy = transform.gameObject.GetComponent<MeshCollider>();
        bool dDead = transform.gameObject.GetComponent<EnemyHealth>().dead;
        if (dDead)
        {
            Destroy(realenemy);
            ragDoll.SetActive(true);
            agent.isStopped = true;
            for (int i = 0; i < body.Length; i++)
            {
                body[i].transform.GetComponent<Rigidbody>().isKinematic = false;
                body[i].transform.GetComponent<Rigidbody>().WakeUp();
            }
        }
    }
    void isAttacking()
    {
        if (Physics.Raycast(head.position, head.forward, out hit, attackLength))
        {
            bool dDead = transform.gameObject.GetComponent<EnemyHealth>().dead;
            if (!dDead)
            {
                if (!attacking)
                {
                    if (hit.transform.tag == "Player")
                    {
                        hit.transform.gameObject.GetComponent<HealthManager>().Health(damage);
                        attacking = true;
                    }
                }
            }
        }
        Debug.DrawRay(head.position, head.forward * attackLength, Color.yellow);
    }
    void iswalking()
    {
        agent = this.GetComponent<NavMeshAgent>();
        agent.SetDestination(player.position);
    }
    void isChasing()
    {
        if (chasing)
        {
            agent.speed = runSpeed;
        }
    }
    void isthinking()
    {
        bool dDead = transform.gameObject.GetComponent<EnemyHealth>().dead;
        if (!dDead)
        {
            if (walking)
            {
                walkTimer -= Time.deltaTime;
                {
                    if (walkTimer <= 0)
                    {
                        walking = false;
                        thinking = true;
                        agent.isStopped = true;
                        walkTimer = maxWalkTimer;
                    }
                }
            }
            if (thinking)
            {
                thinkTimer -= Time.deltaTime;

                if (thinkTimer <= 0)
                {
                    walking = true;
                    thinking = false;
                    agent.isStopped = false;
                    thinkTimer = maxTimer;
                }
            }
        }
    }
}
