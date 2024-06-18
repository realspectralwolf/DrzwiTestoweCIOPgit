using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultsCanvas : MonoBehaviour
{
    [SerializeField] Text timeText;

    void Start()
    {
        
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }
}
