using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int requiredGunSetting = -1;
    [SerializeField] MeshRenderer altMeshRenderer;
    [SerializeField] ParticleSystem altParticleSystem;
    [SerializeField] ParticleSystem completedParticles;
    [SerializeField] ColorPaletteBase colorPalette;
    [SerializeField] float openAnimTime = 1f;
    [SerializeField] GameObject flamesParticles;
    [SerializeField] Collider handleCollider;
    [SerializeField] Collider doorCollider;
    [SerializeField] Room targetRoom;

    public bool isCompleted = false;
    public bool isFailed = false;

    private void Start()
    {
        requiredGunSetting = Random.Range(1, 9);
        altMeshRenderer.material.color = colorPalette.colors[requiredGunSetting];

        ParticleSystem.MainModule mainModule = altParticleSystem.main;
        mainModule.startColor = (Color)colorPalette.colors[requiredGunSetting];
    }

    public void ProcessHit(int gunSetting)
    {
        if (gunSetting > requiredGunSetting)
        {
            GameplayStats.Instance.IncrementError(PlayerError.TooBigSettingForDoors);
            return;
        }

        if (gunSetting < requiredGunSetting)
        {
            GameplayStats.Instance.IncrementError(PlayerError.TooLowSettingForDoors);
            return;
        }

        if (gunSetting == requiredGunSetting)
        {
            // door completed
            altMeshRenderer.material.color = GetComponent<MeshRenderer>().material.color;
            altParticleSystem.gameObject.SetActive(false);
            requiredGunSetting = 0;
            completedParticles.gameObject.SetActive(true);
            AudioManager.Instance.PlaySound("doorUnlocked");
        }
    }

    public void ProcessOnInspectedByUser()
    {
        if (requiredGunSetting == 0 && !isFailed && !isCompleted)
        {
            isCompleted = true;
        }
    }

    public void RequestOpen()
    {
        if (isCompleted)
        {
            // rotate door
            transform.DOLocalRotate(transform.localRotation.eulerAngles + new Vector3(0, -90f, 0), openAnimTime);
            handleCollider.enabled = false;
            GameplayStats.Instance.IncrementDoorsCompleted();
            AudioManager.Instance.PlaySound("doorOpen");
        }
        else
        {
            // Set door on fire
            flamesParticles.gameObject.SetActive(true);
            targetRoom.SetToBlockedRoom();
            FPSHealth.Instance.TakeOneLifeAway();
            isFailed = true;
            handleCollider.enabled = false;
            doorCollider.enabled = false;
            AudioManager.Instance.PlaySound("fire");

            if (requiredGunSetting == 0 && !isCompleted)
            {
                GameplayStats.Instance.IncrementError(PlayerError.NoMeasurement);
            }
        }
    }
}
