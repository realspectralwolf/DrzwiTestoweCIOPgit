using UnityEngine;

[CreateAssetMenu(fileName = "Colors", menuName = "ScriptableObjects/Colors", order = 1)]
public class ColorPaletteBase : ScriptableObject
{
    public Color32[] colors;
}