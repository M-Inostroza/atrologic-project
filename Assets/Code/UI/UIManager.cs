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
        Debug.Log("Showing UI for room: " + roomName);
        switch (roomName)
        {
            case "Workshop":
                UI_panels[0].gameObject.SetActive(true);
                break;
            case "Level selector":
                UI_panels[1].gameObject.SetActive(true);
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
                UI_panels[0].gameObject.SetActive(false);
                break;
            case "Level selector":
                UI_panels[1].gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
}
