using UnityEngine;

[CreateAssetMenu(fileName = "PuzzlePieceData", menuName = "ScriptableObjects/PuzzlePieceData", order = 1)]
public class PuzzlePieceData : ScriptableObject
{
    public float SnapRadius;
    public Vector2 ShrinkSize;
    public Vector2 NormalSize;

    public int ID;
    public Vector2 CorrectPosition;
    public Vector2 InitialPosition;
    public Sprite PieceSprite;
}
