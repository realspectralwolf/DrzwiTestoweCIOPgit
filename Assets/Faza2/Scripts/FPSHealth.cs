using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSHealth : MonoBehaviour
{
    [SerializeField] LifeLostScreen lifeLostScreen;
    public static FPSHealth Instance;

    private void Awake()
    {
        Instance = this;
    }
    public void TakeOneLifeAway()
    {
        GameplayStats.Instance.DecreasePlayerHealth();
        lifeLostScreen.ShowScreen(GameplayStats.Instance.GetPlayerHealth());

        GetComponent<FPSController>().enabled = false;
        GetComponent<CharacterController>().enabled = false;
    }
}
