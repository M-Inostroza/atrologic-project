using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CodeSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        // This is the object being dragged
        GameObject droppedObject = eventData.pointerDrag;
        if (droppedObject == null) return;

        // Move the dragged object into this slot
        droppedObject.transform.SetParent(transform, false);

        // Force the RectTransform to center
        RectTransform droppedRect = droppedObject.GetComponent<RectTransform>();

        // Make sure pivot is (0.5, 0.5) so it's truly centered
        droppedRect.pivot = new Vector2(0.5f, 0.5f);

        // Optionally, also set the anchors to middle if you want
        droppedRect.anchorMin = new Vector2(0.5f, 0.5f);
        droppedRect.anchorMax = new Vector2(0.5f, 0.5f);

        // Now, place it exactly in the center
        droppedRect.anchoredPosition = Vector2.zero;

        Debug.Log($"Dropped {droppedObject.name} into {gameObject.name}.");
    }
}
