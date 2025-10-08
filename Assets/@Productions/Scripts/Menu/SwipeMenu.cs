// SwipeMenuUGUI.cs
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ScrollRect))]
public class SwipeMenuUGUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    public ScrollRect scrollRect;
    public RectTransform content;
    public float snapSpeed = 10f;
    public float scaleFactor = 1.15f;
    public float scaleSmooth = 8f;

    float[] pagePositions;
    RectTransform[] pages;
    bool isDragging;
    float targetPos;

    void Awake()
    {
        if (scrollRect == null) scrollRect = GetComponent<ScrollRect>();
        if (content == null && scrollRect != null) content = scrollRect.content;
    }

    void Start()
    {
        // Ensure layout is up-to-date before measuring
        Canvas.ForceUpdateCanvases();
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(content);

        int totalPages = content.childCount;
        pages = new RectTransform[totalPages];
        pagePositions = new float[Mathf.Max(1, totalPages)];

        float step = totalPages > 1 ? 1f / (totalPages - 1) : 0f;
        for (int i = 0; i < totalPages; i++)
        {
            pagePositions[i] = step * i;
            pages[i] = content.GetChild(i) as RectTransform;
        }

        targetPos = pagePositions.Length > 0 ? pagePositions[0] : 0f;
    }

    void Update()
    {
        if (!isDragging)
        {
            scrollRect.horizontalNormalizedPosition =
                Mathf.Lerp(scrollRect.horizontalNormalizedPosition, targetPos, snapSpeed * Time.deltaTime);
        }
        UpdatePageScaling();
    }

    void UpdatePageScaling()
    {
        float pos = scrollRect.horizontalNormalizedPosition;
        for (int i = 0; i < pages.Length; i++)
        {
            float distance = Mathf.Abs(pos - pagePositions[i]);
            float scale = Mathf.Lerp(scaleFactor, 1f, distance * 4f);
            scale = Mathf.Clamp(scale, 1f, scaleFactor);
            pages[i].localScale = Vector3.Lerp(pages[i].localScale, new Vector3(scale, scale, 1f),
                                               Time.deltaTime * scaleSmooth);
        }
    }

    public void OnBeginDrag(PointerEventData eventData) => isDragging = true;

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        float pos = scrollRect.horizontalNormalizedPosition;
        float closest = pagePositions[0];
        for (int i = 1; i < pagePositions.Length; i++)
            if (Mathf.Abs(pos - pagePositions[i]) < Mathf.Abs(pos - closest))
                closest = pagePositions[i];
        targetPos = closest;
    }

    // public helper to programmatically go to a page
    public void SnapToPage(int index)
    {
        if (index >= 0 && index < pagePositions.Length) targetPos = pagePositions[index];
    }
}
