using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private List<Transform> UI_panels;

    [SerializeField] private BoxCollider2D powerCollider;

    private void Start()
    {
        CollectPanels();
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

    public void ShowRoomUI(string roomName)
    {
        CameraManager.CanMove = false;
        switch (roomName)
        {
            case "Workshop":
                UI_panels[0].gameObject.SetActive(true);
                break;
            case "Level selector":
                UI_panels[1].gameObject.SetActive(true);
                break;
            case "Power generator":
                UI_panels[2].gameObject.SetActive(true);
                powerCollider.enabled = false;
                break;
            case "Market":
                UI_panels[3].gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void HideRoomUI(string roomName)
    {
        switch (roomName)
        {
            case "Workshop":
                UI_panels[0].gameObject.SetActive(false);
                break;
            case "Level selector":
                UI_panels[1].gameObject.SetActive(false);
                break;
            case "Power Generator":
                UI_panels[2].gameObject.SetActive(false);
                powerCollider.enabled = true;
                break;
            case "Market":
                UI_panels[3].gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }


    void CollectPanels()
    {
        UI_panels = new List<Transform>();
        foreach (Transform child in gameObject.transform)
        {
            UI_panels.Add(child);
        }
    }
}
