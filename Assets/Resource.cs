using UnityEngine;
using System;

public class Resource : MonoBehaviour
{
    public static event Action OnResourceCollected;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Scrap");
            OnResourceCollected?.Invoke();
            Destroy(gameObject);
        }
    }
}
