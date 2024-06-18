using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileParticle : MonoBehaviour
{
    [SerializeField] ParticleSystem continuusParticles;
    [SerializeField] ParticleSystem explodeParticles;

    public void Init(Color32 color)
    {
        ChangeParticleColor(continuusParticles, (Color)color);
        ChangeParticleColor(explodeParticles, (Color)color);
        continuusParticles.Play();
    }

    void ChangeParticleColor(ParticleSystem system, Color newColor)
    {
        ParticleSystem.MainModule mainModule = continuusParticles.main;
        mainModule.startColor = newColor;
    }

    public void Explode()
    {
        continuusParticles.Stop();
        explodeParticles.Play();
        Destroy(gameObject, 2);
    }
}
