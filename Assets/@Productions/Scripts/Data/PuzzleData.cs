using UnityEngine;

[CreateAssetMenu(fileName = "PuzzleData", menuName = "Scriptable Objects/PuzzleData")]
public class PuzzleData : ScriptableObject
{
    public Sprite WholeSprite;
    public Sprite[] PiecesSprite;

}
