using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] ClearableSurface[] clearableSurfaces;
    public bool isCompleted = false;

    private void OnEnable()
    {
        for (int i = 0; i < clearableSurfaces.Length; i++)
        {
            clearableSurfaces[i].OnCompleted += CheckIfAllCompleted;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < clearableSurfaces.Length; i++)
        {
            clearableSurfaces[i].OnCompleted -= CheckIfAllCompleted;
        }
    }

    void CheckIfAllCompleted()
    {
        bool areAllCompleted = true;
        for (int i = 0; i < clearableSurfaces.Length; i++)
        {
            if (!clearableSurfaces[i].isCompleted)
            {
                areAllCompleted = false;
                break;
            }
        }

        if (areAllCompleted)
        {
            isCompleted = true;
        }
    }
}