using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoController : MonoBehaviour
{
    public GameObject ammo;
    private ParticleSystem psys;
    private AudioSource audios;

    void Start()
    {
        psys = this.GetComponent<ParticleSystem>();
        audios = this.GetComponent<AudioSource>();
    }
        
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            
            psys.Play();
            audios.Play();
            Destroy(ammo, 1.0f);
        }
    }
}
