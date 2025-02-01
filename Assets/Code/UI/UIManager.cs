using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private List<Transform> UI_panels;

    private void Start()
    {
        UI_panels = new List<Transform>();
        foreach (Transform child in gameObject.transform)
        {
            UI_panels.Add(child);
            Debug.Log($"Panel added to list: {child.name}");
        }
    }

    private void OnEnable()
    {
        RoomEvents.OnRoomEntered += ShowRoomUI;
        RoomEvents.OnRoomLeft += HideRoomUI;
    }

    private void OnDisable()
    {
        RoomEvents.OnRoomEntered -= ShowRoomUI;
        RoomEvents.OnRoomLeft -= HideRoomUI;
    }

    private void ShowRoomUI(string roomName)
    {
        CameraManager.CanMove = false;

        switch (roomName)
        {
            case "Workshop":
                Debug.Log(UI_panels[0]);
                UI_panels[0].gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void HideRoomUI(string roomName)
    {
        CameraManager.CanMove = true;

        switch (roomName)
        {
            case "Workshop":
                Debug.Log("Workshop out");
                UI_panels[0].gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
}
