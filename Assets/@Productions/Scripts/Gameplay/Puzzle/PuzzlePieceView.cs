using UnityEngine;
using DG.Tweening;

public class PuzzlePieceView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public void SetScale(Vector2 size)
    {
        transform.DOScale(size, 0.1f).SetEase(Ease.OutQuad);
    }
}
