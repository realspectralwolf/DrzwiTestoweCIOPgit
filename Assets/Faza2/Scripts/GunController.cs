using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : WeaponPositionController
{
    [SerializeField] Transform mesh;
    [SerializeField] MeshRenderer bodyMeshRend;
    [SerializeField] Transform rotatingGear;
    [SerializeField] Transform shootPoint;
    [SerializeField] Text gunWorldText;
    [SerializeField] Camera targetCamera;
    [SerializeField] LayerMask raycastLayer;
    [SerializeField] float rayDistance = 100f;
    [SerializeField] ProjectileParticle particlePrefab;
    [SerializeField] float particleSpeed = 6f;

    public int currentGunSetting = 0; // <0, 9>

    [SerializeField] ColorPaletteBase colorPalette;

    new void Start()
    {
        base.Start();
        UpdateGunText();
    }

    new void Update()
    {
        base.Update();
        HandleAlphaNumericKeysInput();
    }

    void HandleAlphaNumericKeysInput()
    {
        for (int i = 0; i < 10; i++)
        {
            string key = i.ToString();
            if (Input.GetKeyDown(key))
            {
                SetGunSetting(i);
            }
        }
    }

    void SetGunSetting(int newValue)
    {
        currentGunSetting = newValue;

        rotatingGear.DOKill();
        rotatingGear.DOLocalRotate(rotatingGear.localRotation.eulerAngles + new Vector3(30, 0), 0.2f, RotateMode.Fast);
        UpdateGunText();
    }

    void UpdateGunText()
    {
        gunWorldText.text = currentGunSetting.ToString();
        //gunWorldText.color = colorPalette.colors[currentGunSetting];
        bodyMeshRend.material.color = colorPalette.colors[currentGunSetting];
    }

    public void Shoot()
    {
        Ray ray = new Ray(targetCamera.transform.position, targetCamera.transform.forward);
        RaycastHit hit;

        var particle = Instantiate(particlePrefab, shootPoint.position, Quaternion.identity);
        particle.Init(colorPalette.colors[currentGunSetting]);

        float distance;
        int gunSettingForThisParticle = currentGunSetting;

        if (Physics.Raycast(ray, out hit, rayDistance, raycastLayer))
        {
            distance = Vector3.Distance(particle.transform.position, hit.point);
            float time = distance / particleSpeed;
            particle.transform.DOMove(hit.point, time).SetEase(Ease.Linear).OnComplete(() =>
            {
                particle.Explode();
                AudioManager.Instance.PlaySound("impact");

                if (hit.collider.CompareTag("ClearableSurface"))
                {
                    hit.collider.GetComponent<ClearableSurface>().ProcessHit(gunSettingForThisParticle);
                }

                if (hit.collider.CompareTag("Door"))
                {
                    hit.collider.GetComponent<Door>().ProcessHit(gunSettingForThisParticle);
                }
            });
        }
        else
        {
            distance = 40;
            float time = distance / particleSpeed;
            Vector3 dir = targetCamera.transform.forward;
            Vector3 targetPos = targetCamera.transform.position + dir.normalized * distance;
            particle.transform.DOMove(hit.point, time).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(particle.gameObject);
            });
        }

        AudioManager.Instance.PlaySound("shoot");

        mesh.DOComplete();
        mesh.DOPunchPosition(new Vector3(0, 0, 0.2f), 0.1f, 2);
    }
}
