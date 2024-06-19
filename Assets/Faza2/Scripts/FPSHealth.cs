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

    private void Start()
    {
        AudioManager.Instance.PlaySound("teleport");
    }

    public void TakeOneLifeAway()
    {
        Debug.Log(GameplayStats.Instance.isGameplay);
        if (GameplayStats.Instance.isGameplay)
        {
            GameplayStats.Instance.DecreasePlayerHealth();
            lifeLostScreen.ShowScreen(GameplayStats.Instance.GetPlayerHealth());
        }

        GetComponent<FPSController>().enabled = false;
        GetComponent<CharacterController>().enabled = false;
    }
}
