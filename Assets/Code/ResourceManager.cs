using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private int scrap;
    private TMP_Text scrapCounter;

    public int Scrap
    {
        get { return scrap; }
        private set
        {
            scrap = value;
            UpdateScrapCounter();
        }
    }

    private void OnEnable()
    {
        Resource.OnResourceCollected += HandleResourceCollected;
        LoadScrap();
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
        scrapCounter = GameObject.Find("Scrap counter").GetComponent<TMP_Text>();

        LoadScrap();
    }

    public void AddScrap(int amount)
    {
        Scrap += amount;
    }

    public void RemoveScrap(int amount)
    {
        if (Scrap >= amount)
        {
            Scrap -= amount;
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

    public void UpdateScrapCounter()
    {
        if (scrapCounter != null)
        {
            scrapCounter.text = Scrap.ToString();
        }
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
