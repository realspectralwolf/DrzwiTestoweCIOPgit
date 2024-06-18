using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : WeaponPositionController
{
    [SerializeField] Transform shootPoint;
    [SerializeField] Camera targetCamera;
    [SerializeField] LayerMask raycastLayer;
    [SerializeField] float rayDistance = 100f;
    [SerializeField] ProjectileParticle particlePrefab;
    [SerializeField] float particleSpeed = 6f;
    
    public void Shoot()
    {
        Ray ray = new Ray(targetCamera.transform.position, targetCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, raycastLayer))
        {
            var particle = Instantiate(particlePrefab, shootPoint.position, Quaternion.identity);
            float distance = Vector3.Distance(particle.transform.position, hit.point);
            float time = distance / particleSpeed;
            particle.transform.DOMove(hit.point, time).SetEase(Ease.Linear).OnComplete(() =>
            {
                particle.Explode();
            });
        }
    }
}
