using System;
using UnityEngine;

public class RoomEvents : MonoBehaviour
{
    public static event Action<string> OnRoomEntered;
    public static event Action<string> OnRoomLeft;

    public static void ToggleRoom(string roomName, bool isEntering)
    {
        if (isEntering)
        {
            Debug.Log($"Invoking OnRoomEntered for room: {roomName}");
            OnRoomEntered?.Invoke(roomName);
        }
        else
        {
            Debug.Log($"Invoking OnRoomLeft for room: {roomName}");
            OnRoomLeft?.Invoke(roomName);
        }
    }
}
