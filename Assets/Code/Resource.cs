
using UnityEngine;
using System;

public class Resource : MonoBehaviour
{
    [SerializeField] private string resourceName;

    public static event Action<string> OnResourceCollected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnResourceCollected?.Invoke(resourceName);
            Destroy(gameObject);
        }
    }
    private void OnMouseDown()
    {
        if (resourceName == "Energy")
        {
            Debug.Log(resourceName);
            PowerGenerator.energySpawned = false;
            OnResourceCollected?.Invoke(resourceName);
            Destroy(gameObject);
        }
    }
}