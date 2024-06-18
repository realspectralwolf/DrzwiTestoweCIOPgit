using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorsCanvas : MonoBehaviour
{
    [SerializeField] Text noMeasurement;
    [SerializeField] Text tooHighWhenShootingDoor;
    [SerializeField] Text tooLowWhenShootingDor;
    [SerializeField] Text hitWallsWithWrongSetting;
    [SerializeField] Text skippedWalls;
    [SerializeField] Text wallsHitMoreThanOnce;

    void Start()
    {
        
    }
}
