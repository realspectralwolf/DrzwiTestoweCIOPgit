using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultsCanvas : MonoBehaviour
{
    [SerializeField] Text timeText;
    [SerializeField] Text doorsCompletedText;
    [SerializeField] Text roomsCompletedText;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        timeText.text = GameplayStats.Instance.GetTimeString();
        doorsCompletedText.text = GameplayStats.Instance.GetCompletedDoors().ToString();
        roomsCompletedText.text = GameplayStats.Instance.GetRoomsCompleted().ToString();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }
}
