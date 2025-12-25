using UnityEngine;
using UnityEngine.UI;

public class PieceView : MonoBehaviour
{
    [SerializeField] private Image image;

    public void UpdateImage(Sprite sprite)
    {
        image.sprite = sprite;
    }
}
