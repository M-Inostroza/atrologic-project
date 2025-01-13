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
            OnRoomEntered?.Invoke(roomName);
        }
        else
        {
            OnRoomLeft?.Invoke(roomName);
        }
    }
}
