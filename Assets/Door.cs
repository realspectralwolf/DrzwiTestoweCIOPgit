using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int requiredGunSetting = -1;
    [SerializeField] MeshRenderer altMeshRenderer;
    [SerializeField] ColorPaletteBase colorPalette;

    private void Start()
    {
        requiredGunSetting = Random.Range(1, 9);
        altMeshRenderer.material.color = colorPalette.colors[requiredGunSetting];
    }
}
