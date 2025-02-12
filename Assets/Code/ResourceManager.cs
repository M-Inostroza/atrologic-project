using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private int scrap;
    private TMP_Text scrapCounter;

    private int energy;
    private int energyCap = 800;
    private int energyGain = 40;
    private TMP_Text energyCounter;

    public int Energy
    {
        get { return energy; }
        private set
        { energy = value; }
    }

    public int Scrap
    {
        get { return scrap; }
        private set
        { scrap = value; }
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
        switch (resourceName)
        {
            case "Scrap":
                AddScrap(1);
                break;
            case "Energy":
                AddEnergy(energyGain);
                break;
        }
    }

    private void Start()
    {
        scrapCounter = GameObject.Find("Scrap counter").GetComponent<TMP_Text>();
        energyCounter = GameObject.Find("Energy").GetComponent<TMP_Text>();

        LoadScrap();
        LoadEnergy();
    }

    public void AddScrap(int amount)
    {
        Scrap += amount;
        UpdateScrapCounter();
    }

    public void AddEnergy(int amount)
    {
        if (Energy + amount <= energyCap)
        {
            Energy += amount;
            SaveEnergy();
            UpdateEnergyCounter();
        }
        else
        {
            Debug.LogWarning("Energy cap reached.");
        }
    }

    public void RemoveScrap(int amount)
    {
        if (Scrap >= amount)
        {
            Scrap -= amount;
            SaveScrap();
            UpdateScrapCounter();
        }
        else
        {
            Debug.LogWarning("Not enough Scrap to remove.");
        }
    }

    public void ResetScrap()
    {
        Scrap = 0;
        UpdateScrapCounter();
    }

    public void UpdateScrapCounter()
    {
        if (scrapCounter != null)
        {
            scrapCounter.text = Scrap.ToString();
        }
    }

    public void UpdateEnergyCounter()
    {
        if (energyCounter != null)
        {
            energyCounter.text = Energy.ToString();
        }
    }

    private void SaveScrap()
    {
        ES3.Save("Scrap", Scrap);
    }

    private void SaveEnergy()
    {
        ES3.Save("Energy", Energy);
    }

    private void LoadScrap()
    {
        if (ES3.KeyExists("Scrap"))
        {
            Scrap = ES3.Load<int>("Scrap");
            UpdateScrapCounter();
        }
        else
        {
            Scrap = 0;
        }
    }

    private void LoadEnergy()
    {
        if (ES3.KeyExists("Energy"))
        {
            Energy = ES3.Load<int>("Energy");
            UpdateEnergyCounter();
        }
        else
        {
            Energy = 0;
        }
    }

    private void OnApplicationQuit()
    {
        SaveScrap();
        SaveEnergy();
    }
}
