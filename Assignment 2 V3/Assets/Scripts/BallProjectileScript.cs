using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallProjectileScript : MonoBehaviour
{
    public GameObject projectile; //the ball projectile reference
    public float timeToLive;
    public PhysicMaterial materialChange;
    
    private AudioSource audiosrc; //the audio source related to this ball projectile
    private SphereCollider sphereCol; //the sphere collider related to this ball projectile

    void Start()
    {
        audiosrc = this.GetComponent<AudioSource>();
        sphereCol = this.GetComponent<SphereCollider>();
    }

    public void ChangeMaterial()
    {
        this.sphereCol.material = materialChange;
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && projectile.CompareTag("LiveAmmo"))
        {
            audiosrc.Play();
            Destroy(projectile, timeToLive);
        }
    }
}
