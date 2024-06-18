using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSHealth : MonoBehaviour
{
    [SerializeField] LifeLostScreen lifeLostScreen;
    public static FPSHealth Instance;
    int health = 3;

    private void Awake()
    {
        Instance = this;
    }
    public void TakeOneLifeAway()
    {
        StartCoroutine(TakeLifeAwaySequence());
    }

    IEnumerator TakeLifeAwaySequence()
    {
        health--;
        lifeLostScreen.ShowScreen(health);
        yield return new WaitForSeconds(6);
        PlayerRespawner.Instance.RespawnPlayer();
    }
}
