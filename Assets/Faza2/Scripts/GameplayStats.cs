using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayStats : MonoBehaviour
{
    public float gameplaySeconds = 0;
    public int doorsCompleted = 0;
    public int roomsCompleted = 0;

    public static GameplayStats Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        gameplaySeconds += Time.unscaledDeltaTime;
    }
}
