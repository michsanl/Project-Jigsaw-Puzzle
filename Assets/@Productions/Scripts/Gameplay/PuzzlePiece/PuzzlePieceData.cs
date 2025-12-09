using UnityEngine;

[CreateAssetMenu(fileName = "PieceData", menuName = "Scriptable Objects/PieceData")]
public class PuzzlePieceData : ScriptableObject
{
    public string pieceId;

    [Header("Addressables Keys")]
    public string pieceSpriteKey;       // e.g. "piece_01_sprite"
    public string piecePrefabKey;       // optional, usually same prefab reused

    [Header("Settings")]
    public float snapDistance = 150f;
}
