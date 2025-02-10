using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    LevelUIManager levelUIManager;
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

    private void HandleResourceCollected(string resourceName)
    {
        if (resourceName == "Scrap")
        {
            AddScrap(1);
        }
    }

    private void Start()
    {
        Scrap = 0;
        levelUIManager = FindFirstObjectByType<LevelUIManager>();
        LoadScrap();
        levelUIManager.UpdateScrapCounter(Scrap);
    }

    public void AddScrap(int amount)
    {
        Scrap += amount;
        levelUIManager.UpdateScrapCounter(Scrap);
    }

    public void RemoveScrap(int amount)
    {
        if (Scrap >= amount)
        {
            Scrap -= amount;
            levelUIManager.UpdateScrapCounter(Scrap);
        }
        else
        {
            Debug.LogWarning("Not enough Scrap to remove.");
        }
    }

    private void OnApplicationQuit()
    {
        SaveScrap();
    }

    private void SaveScrap()
    {
        ES3.Save("Scrap", Scrap);
    }

    private void LoadScrap()
    {
        if (ES3.KeyExists("Scrap"))
        {
            Scrap = ES3.Load<int>("Scrap");
        }
        else
        {
            Scrap = 0;
        }
    }
}
