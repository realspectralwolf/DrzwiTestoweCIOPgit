using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeLostScreen : MonoBehaviour
{
    [SerializeField] Image backgroundImage;
    [SerializeField] Text title;

    void ShowScreen(int livesLeft)
    {
        string text;
        if (livesLeft == 0)
        {
            text = "BRAK ŻYĆ. KONIEC.";
        }
    }
}
