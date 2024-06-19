using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerError
{
    NoMeasurement,
    TooBigSettingForDoors,
    TooLowSettingForDoors,
    WrongSettingForSurface,
    SkippedWalls,
    SurfacesHitMoreThanOnce
}

public class GameplayStats : MonoBehaviour
{
    public float gameplaySeconds = 0;
    public int doorsCompleted = 0;
    public Room[] rooms;
    public List<ClearableSurface> allSurfaces { get; private set; } = new();
    public List<ClearableSurface> completedSurfaces { get; private set; } = new();
    public List<ClearableSurface> blockedSurfaces { get; private set; } = new();

    public static GameplayStats Instance;

    /*    Gracz dostaje informacje o:
    - Czasie działania
    - Liczbie poprawnie zneutralizowanych drzwi
    - Liczbie poprawnie zneutralizowanych pomieszczeń
    Popełnionych błędach:
    - Brak wykonania pomiaru
    - Za duże ustawienie pierścienia podczas neutralizacji drzwi
    - Za małe ustawienie pierścienia podczas neutralizacji drzwi
    - Złe ustawienie pierścienia podczas strzelania w pomieszczeniu za drzwiami
    - Pominięte ściany w pomieszczeniu za drzwiami
    - Liczba ścian w pomieszczeniu za drzwiami trafionych więcej niż raz*/

    Dictionary<PlayerError, int> errorsDictionary = new Dictionary<PlayerError, int>
    {
        { PlayerError.NoMeasurement, 0 },
        { PlayerError.TooBigSettingForDoors, 0 },
        { PlayerError.TooLowSettingForDoors, 0 },
        { PlayerError.WrongSettingForSurface, 0 },
        { PlayerError.SkippedWalls, 0 },
        { PlayerError.SurfacesHitMoreThanOnce, 0 }
    };

    public void IncrementError(PlayerError error)
    {
        errorsDictionary[error] += 1;
    }

    public int GetErrorCount(PlayerError error)
    {
        return errorsDictionary[error];
    }

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
        CheckIfCompletedAll();
    }

    public void AddToBlockedSurfaces(ClearableSurface newSurface)
    {
        if (blockedSurfaces.Contains(newSurface)) return;
        blockedSurfaces.Add(newSurface);
        CheckIfCompletedAll();
    }

    void CheckIfCompletedAll()
    {
        if ((completedSurfaces.Count + blockedSurfaces.Count) == allSurfaces.Count)
        {
            // Game Completed
            // Progress to Phase 3 (results)
            LoadResultsScene();
        }
    }

    public void LoadResultsScene()
    {
        UpdateSomeStatistics();
        SceneManager.LoadScene(2);
    }

    void UpdateSomeStatistics()
    {
        int skippedWalls = 0;
        for (int i = 0; i < allSurfaces.Count; i++)
        {
            if (!completedSurfaces.Contains(allSurfaces[i])) skippedWalls++;
        }
        errorsDictionary[PlayerError.SkippedWalls] = skippedWalls;

        int wallsHitMoreThanOnce = 0;
        for (int i = 0; i < completedSurfaces.Count; i++)
        {
            if (completedSurfaces[i].hitCount > 1) wallsHitMoreThanOnce++;
        }
        errorsDictionary[PlayerError.SurfacesHitMoreThanOnce] = wallsHitMoreThanOnce;
    }
}
