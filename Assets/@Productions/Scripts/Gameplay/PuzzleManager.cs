using DG.Tweening;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private float snapRadius = 30;

    public static PuzzleManager Instance;
    public PuzzleSlot[] slots;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public bool CheckPiecePlacement(PuzzlePiece piece)
    {
        foreach (var slot in slots)
        {
            if (slot.ID != piece.ID)
                continue;
            if (GetDistanceToSlot(piece, slot) > snapRadius) // tolerance in pixels
                continue;

            // Snap piece into correct slot
            piece.transform.DOMove(slot.transform.position, 0.5f).SetEase(Ease.OutQuad);
            //piece.GetComponent<RectTransform>()
            //    .DOLocalMove(slot.transform.position, 0.5f)
            //    .SetEase(Ease.OutQuad);
            Debug.Log($"Piece {piece.ID} placed correctly!");
            return true;
        }
        return false;
    }

    private static float GetDistanceToSlot(PuzzlePiece piece, PuzzleSlot slot)
    {

        // Check if piece overlaps slot in screen space
        return Vector2.Distance(
            piece.GetComponent<RectTransform>().position,
            slot.rectTransform.position
        );
    }

    private bool IsOverlapping(RectTransform rectA, RectTransform rectB)
    {
        Rect a = GetScreenRect(rectA);
        Rect b = GetScreenRect(rectB);
        return a.Overlaps(b);
    }

    private Rect GetScreenRect(RectTransform rectTransform)
    {
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        return new Rect(corners[0], corners[2] - corners[0]);
    }
}
