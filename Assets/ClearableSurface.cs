using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearableSurface : MonoBehaviour
{
    float animTime = 0.4f;
    MeshRenderer meshRend;
    Color32 initialColor;

    int hitCount = 0;

    private void Start()
    {
        meshRend = GetComponent<MeshRenderer>();
        initialColor = meshRend.material.color;
    }

    public void ProcessHit()
    {
        hitCount++;

        if (hitCount > 1)
        {
            // too many
        }

        Color targetColor = new Color32(52, 235, 64, 255);
        meshRend.material.DOKill();
        meshRend.material.DOColor(targetColor, animTime).OnComplete(() =>
        {
            meshRend.material.DOColor(initialColor, animTime);
        });
    }
}
