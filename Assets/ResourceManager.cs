using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private int scrap;

    public int Scrap
    {
        get { return scrap; }
        private set { scrap = value; }
    }

    private void OnEnable()
    {
        Resource.OnResourceCollected += HandleResourceCollected;
    }

    private void OnDisable()
    {
        Resource.OnResourceCollected -= HandleResourceCollected;
    }

    private void HandleResourceCollected()
    {
        AddScrap(1);
    }

    private void Start()
    {
        Scrap = 0;
    }

    public void AddScrap(int amount)
    {
        Scrap += amount;
        Debug.Log($"Scrap added: {amount}. Total Scrap: {Scrap}");
    }

    public void RemoveScrap(int amount)
    {
        if (Scrap >= amount)
        {
            Scrap -= amount;
            Debug.Log($"Scrap removed: {amount}. Total Scrap: {Scrap}");
        }
        else
        {
            Debug.LogWarning("Not enough Scrap to remove.");
        }
    }
}
