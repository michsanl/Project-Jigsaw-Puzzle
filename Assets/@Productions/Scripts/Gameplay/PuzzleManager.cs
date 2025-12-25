using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private float snapRadius = 30;
    [SerializeField] private PuzzleSlot[] slots;
    [SerializeField] private PuzzlePiece[] pieces;
    [SerializeField] private PuzzleData[] datas;
    [SerializeField] private Image puzzleImage;

    public static PuzzleManager Instance;

    private PuzzleData selectedData;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        InitializePuzzle();
    }

    private void InitializePuzzle()
    {
        selectedData = datas[PuzzleSelection.SelectedPuzzleIndex];
        Debug.Log($"Selected puzzle index is: {selectedData}");
        InitializePuzzleImage();
        InitializePuzzlePieces();
    }

    public bool CheckPiecePlacement(PuzzlePiece piece)
    {
        foreach (var slot in slots)
        {
            if (slot.ID != piece.ID)
                continue;
            if (GetDistanceToSlot(piece, slot) > snapRadius) // tolerance in pixels
                continue;

            piece.transform.DOMove(slot.transform.position, 0.5f).SetEase(Ease.OutQuad);
            return true;
        }
        return false;
    }

    private void InitializePuzzleImage()
    {
        puzzleImage.sprite = selectedData.WholeSprite;
    }

    private void InitializePuzzlePieces()
    {
        int index;
        for (int i = 0; i < selectedData.PiecesSprite.Length; i++)
        {
            index = i;
            pieces[index].Initialize(index, selectedData.PiecesSprite[index]);
        }
    }

    private static float GetDistanceToSlot(PuzzlePiece piece, PuzzleSlot slot)
    {
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
