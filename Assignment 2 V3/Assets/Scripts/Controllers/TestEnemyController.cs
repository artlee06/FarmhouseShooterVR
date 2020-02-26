using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemyController : MonoBehaviour
{
    public float lookRadius;
    public float attackingRadius;
    public GameObject enemyObject;
    public ScoringSystem scoringSys;
    public int points;
    public int lives;
    private bool isDead = false;

    Transform target;
    NavMeshAgent agent;
    Animator animator;
    AudioSource audioSrc;
    private new Rigidbody rigidbody;
    public float speed = 1.0f;


    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        audioSrc = GetComponent<AudioSource>();
    }

    public bool GetIsDead()
    {
        return this.isDead;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "LiveAmmo")
        {
            animator.SetBool("GetHit", true);
            lives -= 1;
            StopMovement();
            Debug.Log("Enemy hit");
            if (lives == 0) {
                Die();
            }
        }
    }

    private void AddPoints()
    {
        scoringSys.GetComponent<ScoringSystem>().UpdateScore(points);
    }

    private void Die()
    {
        isDead = true;
        Destroy(enemyObject, 2.0f);
        AddPoints();
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            FaceTarget();
            agent.SetDestination(target.position);
            animator.SetBool("WithinSpottingDistance", true);

            if (distance <= attackingRadius)
            {
                FaceTarget();
                Attack();
            }
        }

        else
        {
            animator.SetBool("WithinSpottingDistance", false);
        }
    }

    private void Attack()
    {
        agent.destination = target.position;
        animator.SetBool("WithinAttackingDistance", true);
    }

    private void StopMovement()
    {
        rigidbody.isKinematic = true;
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);

    }
}
