using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopCharacter : MonoBehaviour
{
    [SerializeField] List<LaserBodyPart> bodyPartsRequired;
    [SerializeField] LaserBodyPart handRight;

    private void OnEnable()
    {
        for (int i = 0; i < bodyPartsRequired.Count; i++)
        {
            bodyPartsRequired[i].OnStatusChanged += CheckIfAllBodyPartsCompleted;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < bodyPartsRequired.Count; i++)
        {
            bodyPartsRequired[i].OnStatusChanged -= CheckIfAllBodyPartsCompleted;
        }
    }

    void CheckIfAllBodyPartsCompleted()
    {
        bool allCompleted = true;
        for (int i = 0; i < bodyPartsRequired.Count; i++)
        {
            if (bodyPartsRequired[i].heldItem == null)
            {
                allCompleted = false;
                break;
            }
        }

        if (allCompleted)
        {
            WeaponControls.doFlipHands = (handRight.heldItem.itemType == InteractableItem.Tool);
            // Proceed to next level
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
