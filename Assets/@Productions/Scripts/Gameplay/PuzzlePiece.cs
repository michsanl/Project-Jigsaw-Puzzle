using DG.Tweening;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePiece : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private PieceView view;

    public int ID = 0;
    public float snapRadius = 100f;

    private Sprite sprite;
    private Vector2 initialPosition;
    private Vector2 correctPosition;
    private RectTransform rectTransform;
    private Canvas canvas;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        initialPosition = transform.localPosition;
    }

    public void Initialize(int id, Sprite sprite)
    {
        ID = id;
        this.sprite = sprite;

        view.UpdateImage(sprite);
        correctPosition = PuzzleManager.Instance.GetSlotPosition(ID);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint
        );

        rectTransform.DOAnchorPos(localPoint, 0.2f).SetEase(Ease.OutQuad);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint
        );
        
        rectTransform.DOAnchorPos(localPoint, 0.2f).SetEase(Ease.OutQuad);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //PuzzleManager.Instance.CheckPiecePlacement(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Vector2.Distance(rectTransform.position, correctPosition) < snapRadius)
        {
            rectTransform.DOAnchorPos(correctPosition, 0.5f).SetEase(Ease.OutQuad);
        }
        else
        {
            rectTransform.DOAnchorPos(initialPosition, 0.2f).SetEase(Ease.OutQuad);
        }
    }
}
