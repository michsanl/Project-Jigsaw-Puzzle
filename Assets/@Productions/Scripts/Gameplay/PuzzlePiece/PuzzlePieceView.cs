using UnityEngine;
using UnityEngine.UI;

public class PuzzlePieceView : MonoBehaviour
{
    [SerializeField] private Image image;

    public void UpdateImage(Sprite sprite)
    {
        image.sprite = sprite;
    }
}
