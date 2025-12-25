using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePieceController : MonoBehaviour,
    IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private PuzzlePieceView view;
    [SerializeField] private PuzzlePieceData data;

    private Vector3 correctPosition;
    private Vector3 initialPosition;
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
        initialPosition = transform.position;
        correctPosition = Vector2.zero;

        view.SetScale(data.ShrinkSize);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector3 worldPos = cam.ScreenToWorldPoint(eventData.position);
        worldPos.z = 0f;

        transform.DOKill();
        transform.DOMove(worldPos, 0.1f)
                 .SetEase(Ease.OutQuad);
        view.SetScale(data.NormalSize);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 worldPos = cam.ScreenToWorldPoint(eventData.position);
        worldPos.z = 0f;

        transform.DOMove(worldPos, 0.1f)
                 .SetEase(Ease.OutQuad);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Vector2.Distance(transform.position, correctPosition) <= data.SnapRadius)
        {
            transform.DOMove(correctPosition, 0.25f)
                     .SetEase(Ease.OutQuad);
        }
        else
        {
            transform.DOMove(initialPosition, 0.1f)
                     .SetEase(Ease.OutQuad);
            view.SetScale(data.ShrinkSize);
        }
    }
}
