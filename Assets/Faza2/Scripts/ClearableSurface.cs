using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearableSurface : MonoBehaviour
{
    float animTime = 0.3f;
    MeshRenderer meshRend;
    Color32 initialColor;

    public System.Action OnCompleted;
    public bool isCompleted = false;

    private void Start()
    {
        meshRend = GetComponent<MeshRenderer>();
        initialColor = meshRend.material.color;

        GameplayStats.Instance.AddToAllSurfaces(this);
    }

    public void ProcessHit(int gunSetting)
    {
        if (gunSetting == 0 && !isCompleted)
        {
            isCompleted = true;

            Color targetColor = new Color32(52, 235, 64, 255);
            meshRend.material.DOKill();
            meshRend.material.DOColor(targetColor, animTime).OnComplete(() =>
            {
                meshRend.material.DOColor(initialColor, animTime);
            });

            GameplayStats.Instance.AddToCompletedSurfaces(this);
            OnCompleted?.Invoke();
        }

        if (gunSetting != 0)
        {
            // Wrong gun setting
            return;
        }

        if (isCompleted)
        {
            // Too Many Mistake
        }
    }
}
