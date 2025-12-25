using UnityEngine;

public class PuzzleSlot : MonoBehaviour
{
    public int ID = 0;

    [SerializeField] public RectTransform rectTransform { get; private set; }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

}
