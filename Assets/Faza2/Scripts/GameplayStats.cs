using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayStats : MonoBehaviour
{
    public float gameplaySeconds = 0;
    public int doorsCompleted = 0;
    public Room[] rooms;
    public List<ClearableSurface> allSurfaces { get; private set; } = new();
    public List<ClearableSurface> completedSurfaces { get; private set; } = new();

    public static GameplayStats Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        gameplaySeconds += Time.unscaledDeltaTime;
    }

    public void AddToAllSurfaces(ClearableSurface newSurface)
    {
        if (allSurfaces.Contains(newSurface)) return;
        allSurfaces.Add(newSurface);
    }

    public void AddToCompletedSurfaces(ClearableSurface newSurface)
    {
        if (completedSurfaces.Contains(newSurface)) return;

        completedSurfaces.Add(newSurface);

        if (completedSurfaces.Count == allSurfaces.Count)
        {
            // Game Completed
            // Progress to Phase 3 (results)
        }
    }

    public void LoadResultsScene()
    {
        SceneManager.LoadScene(2);
    }
}
