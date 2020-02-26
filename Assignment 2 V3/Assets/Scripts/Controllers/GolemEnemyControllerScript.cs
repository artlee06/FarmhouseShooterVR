using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemEnemyControllerScript : MonoBehaviour
{
    public float lookRadius;
    public GameObject enemyObject;
    public ScoringSystem scoringSys;
    public int points;
    private bool isDead = false;

    Transform target;
    Animator animator;
    AudioSource audioSrc;


    void Start()
    {
        target = PlayerManager.instance.player.transform;
        animator = GetComponent<Animator>();
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
            audioSrc.Play();
            Debug.Log("Golem hit");
            Die();
            AddPoints();
        }
    }

    private void AddPoints()
    {
        scoringSys.GetComponent<ScoringSystem>().UpdateScore(points);
    }

    private void Die()
    {
        Destroy(enemyObject, 6.0f);
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            FaceTarget();
            Attack();
        }
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
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
