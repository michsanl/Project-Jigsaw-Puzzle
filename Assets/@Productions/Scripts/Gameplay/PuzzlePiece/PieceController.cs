using DG.Tweening;
using DG.Tweening.Core; // Optional, for DOTween extension methods
using DG.Tweening.Plugins.Options; // Optional, for DOTween extension methods
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PieceController : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private PieceData data;
    [SerializeField] private PieceView view;

    public int ID = 0;

    private Sprite sprite;
    private Vector2 initialPosition;
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
        if (PuzzleManager.Instance.CheckPiecePlacement(this))
        {
            return;
        }

        rectTransform.DOAnchorPos(initialPosition, 0.2f).SetEase(Ease.OutQuad);
    }
}
