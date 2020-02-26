using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireAction : MonoBehaviour
{
    public float speed;
    public int damage;

    Vector3 shootDirection;

    void FixedUpdate()
    {
        this.transform.Translate(shootDirection * speed, Space.World);
    }

    public void FireProjectile(Ray shootRay)
    {
        this.shootDirection = shootRay.direction;
        this.transform.position = shootRay.origin;
        rotateInShootDirection();
    }
    /*
    void OnCollisionEnter(Collision col)
    {
        Enemy enemy = col.collider.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.TakeDamage(damage);
        }

        Destroy(this.gameObject);
    }
    */

    void OnTriggerEnter(Collider col)
    {
        //all projectile colliding game objects should be tagged "Enemy" or whatever in inspector but that tag must be reflected in the below if conditional
        if (col.gameObject.tag == "Enemy")
        {
            Destroy(col.gameObject);

            //add an explosion or something
            //destroy the projectile that just caused the trigger collision
            Destroy(this.gameObject);
        }
    }

    void rotateInShootDirection()
    {
        Vector3 newRotation = Vector3.RotateTowards(transform.forward, shootDirection, 0.01f, 0.0f);
        transform.rotation = Quaternion.LookRotation(newRotation);
    }
}
