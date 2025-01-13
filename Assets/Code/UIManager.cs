using UnityEngine;

public class UIManager : MonoBehaviour
{
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
        Debug.Log($"Displaying UI for room: {roomName}");
        // Logic to display the room's UI
    }

    private void HideRoomUI(string roomName)
    {
        Debug.Log($"Hiding UI for room: {roomName}");
        // Logic to hide the room's UI
    }
}
