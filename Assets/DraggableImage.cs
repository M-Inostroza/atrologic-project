using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class DraggableImage : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public BaseNode nodeData; // The data this image represents

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private RectTransform parentCanvasRect; // The RectTransform of the Canvas
    private Vector2 offset;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();

        // Find the top-level Canvas (assuming the image is a child of it or one of its children).
        Canvas parentCanvas = GetComponentInParent<Canvas>();
        if (parentCanvas != null)
        {
            parentCanvasRect = parentCanvas.GetComponent<RectTransform>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Make the dragged item not block raycasts while dragging
        canvasGroup.blocksRaycasts = false;

        // Convert the screen point to local canvas space
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvasRect,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPointerPos);

        // Calculate the offset so the mouse picks up the image exactly where it clicked
        offset = rectTransform.anchoredPosition - localPointerPos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Keep converting the current screen pointer position to the Canvas's local space
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvasRect,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPointerPos))
        {
            // Apply the offset so the image stays under the mouse
            rectTransform.anchoredPosition = localPointerPos + offset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Re-enable raycasts
        canvasGroup.blocksRaycasts = true;
    }
}
