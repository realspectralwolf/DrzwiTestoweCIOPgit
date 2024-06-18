using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileParticle : MonoBehaviour
{
    [SerializeField] ParticleSystem continuusParticles;
    [SerializeField] ParticleSystem explodeParticles;

    private void Start()
    {
        continuusParticles.Play();
    }

    public void Explode()
    {
        continuusParticles.Stop();
        explodeParticles.Play();
        Destroy(gameObject, 2);
    }
}
