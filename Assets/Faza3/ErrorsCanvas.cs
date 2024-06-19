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
        noMeasurement.text = GameplayStats.Instance.GetErrorCount(PlayerError.NoMeasurement).ToString();
        tooHighWhenShootingDoor.text = GameplayStats.Instance.GetErrorCount(PlayerError.TooBigSettingForDoors).ToString();
        tooLowWhenShootingDor.text = GameplayStats.Instance.GetErrorCount(PlayerError.TooLowSettingForDoors).ToString();
        hitWallsWithWrongSetting.text = GameplayStats.Instance.GetErrorCount(PlayerError.WrongSettingForSurface).ToString();
        skippedWalls.text = GameplayStats.Instance.GetErrorCount(PlayerError.SkippedWalls).ToString();
        wallsHitMoreThanOnce.text = GameplayStats.Instance.GetErrorCount(PlayerError.SurfacesHitMoreThanOnce).ToString();
    }
}
