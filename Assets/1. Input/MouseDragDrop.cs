using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseDragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Drag Drop Speed")]
    public float speed = 1f;

    private Image image;
    private RectTransform rectTransform;
    private BoxCollider2D boxCollider2D;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (boxCollider2D == null)
        {
            boxCollider2D = gameObject.AddComponent<BoxCollider2D>();
        }
        if (image == null)
        {
            image = gameObject.AddComponent<Image>();
        }
        if (rectTransform == null)
        {
            rectTransform = gameObject.GetComponent<RectTransform>();
            if (rectTransform == null)
            {
                rectTransform = gameObject.AddComponent<RectTransform>();
            }
        }
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor * speed;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}