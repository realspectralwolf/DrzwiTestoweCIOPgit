using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawner : MonoBehaviour
{
    [SerializeField] FPSController currentPlayer;
    [SerializeField] FPSController playerPrefab;
    public static PlayerRespawner Instance;
    private void Awake()
    {
        Instance = this;
    }

    public void RespawnPlayer()
    {
        if (currentPlayer != null) Destroy(currentPlayer.gameObject);
        currentPlayer = Instantiate(playerPrefab, transform.position, transform.rotation, transform.parent);
    }
}
