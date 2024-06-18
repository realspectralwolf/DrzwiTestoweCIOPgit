using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeLostScreen : MonoBehaviour
{
    [SerializeField] Image backgroundImage;
    [SerializeField] Text title;
    [SerializeField] Image skullImage;
    [SerializeField] float animTime = 1;

    public void ShowScreen(int livesLeft)
    {
        backgroundImage.color = new Color32(0, 0, 0, 0);
        backgroundImage.enabled = true;
        backgroundImage.DOColor(new Color32(0, 0, 0, 204), animTime);

        title.transform.localScale = Vector3.zero;
        title.enabled = true;
        title.transform.DOScale(Vector3.one, animTime).SetEase(Ease.OutBack).SetDelay(animTime);

        skullImage.enabled = true;
        var targetPos = skullImage.transform.localPosition;
        skullImage.transform.localPosition = Vector3.zero;
        skullImage.transform.DOLocalMove(targetPos, animTime);

        skullImage.color = new Color32(255, 255, 255, 255);
        skullImage.enabled = true;
        skullImage.DOColor(new Color32(255, 255, 255, 0), animTime * 2).SetDelay(0);

        string text;
        switch (livesLeft)
        {
            case 0:
                text = "BRAK ŻYĆ. KONIEC.";
                break;
            case 1:
                text = "POZOSTAŁO 1. ŻYCIE";
                break;
            case 2:
                text = "POZOSTAŁY 2. ŻYCIA";
                break;
            default:
                text = "STRACIŁEŚ ŻYCIE.";
                break;
        }
        title.text = text;
    }

    public void HideScreen()
    {
        backgroundImage.enabled = false;
        title.enabled = false;
        skullImage.enabled = false;
    }
}
