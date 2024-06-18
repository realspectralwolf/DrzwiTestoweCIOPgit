using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : WeaponPositionController
{
    [SerializeField] Transform mesh;
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

    private void Start()
    {
        base.Start();
        UpdateGunText();
    }

    void Update()
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
        gunWorldText.color = colorPalette.colors[currentGunSetting];
    }

    public void Shoot()
    {
        Ray ray = new Ray(targetCamera.transform.position, targetCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, raycastLayer))
        {
            var particle = Instantiate(particlePrefab, shootPoint.position, Quaternion.identity);
            particle.Init(colorPalette.colors[currentGunSetting]);

            float distance = Vector3.Distance(particle.transform.position, hit.point);
            float time = distance / particleSpeed;
            int gunSettingForThisParticle = currentGunSetting;
            particle.transform.DOMove(hit.point, time).SetEase(Ease.Linear).OnComplete(() =>
            {
                particle.Explode();

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

        mesh.DOComplete();
        mesh.DOPunchPosition(new Vector3(0, 0, 0.2f), 0.1f, 2);
    }
}
