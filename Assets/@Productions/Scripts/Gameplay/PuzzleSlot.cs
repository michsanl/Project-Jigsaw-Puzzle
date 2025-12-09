using UnityEngine;

public class PuzzleSlot : MonoBehaviour
{
    public int ID = 0;

    public RectTransform rectTransform { get; private set; }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

}
