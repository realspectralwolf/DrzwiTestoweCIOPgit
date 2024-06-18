using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int requiredGunSetting = -1;
    [SerializeField] MeshRenderer altMeshRenderer;
    [SerializeField] ParticleSystem altParticleSystem;
    [SerializeField] ColorPaletteBase colorPalette;

    private void Start()
    {
        requiredGunSetting = Random.Range(1, 9);
        altMeshRenderer.material.color = colorPalette.colors[requiredGunSetting];
    }

    public void ProcessHit(int gunSetting)
    {
        if (gunSetting == requiredGunSetting)
        {
            // door completed
            altMeshRenderer.material.color = GetComponent<MeshRenderer>().material.color;
            altParticleSystem.gameObject.SetActive(false);
            requiredGunSetting = 0;
        }
    }
}
