using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private int scrap;
    private TMP_Text scrapCounter;

    private float energy;
    private int energyCap = 800;
    private int energyGain = 40;
    private TMP_Text energyCounter;

    public float Energy
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
        GameManager.OnSceneChanged += HandleSceneChanged;
    }

    private void OnDisable()
    {
        Resource.OnResourceCollected -= HandleResourceCollected;
        GameManager.OnSceneChanged -= HandleSceneChanged;
    }

    private void HandleResourceCollected(string resourceName)
    {
        switch (resourceName)
        {
            case "Scrap":
                AddScrap(10);
                break;
            case "Energy":
                AddEnergy(energyGain);
                break;
        }
    }

    private void HandleSceneChanged(GameManager.GameState newState)
    {
        SaveScrap();
        SaveEnergy();
    }

    private void Start()
    {
        scrapCounter = GameObject.Find("Scrap counter").GetComponent<TMP_Text>();
        energyCounter = GameObject.Find("Energy counter").GetComponent<TMP_Text>();
        LoadScrap();
        LoadEnergy();
    }

    public void AddScrap(int amount)
    {
        Scrap += amount;
        UpdateScrapCounter();
    }

    public void AddEnergy(float amount)
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

    public void RemoveEnergy(float amount)
    {
        if (Energy >= amount)
        {
            Energy -= amount * Time.deltaTime;
            // remove energy with do tween
            
            UpdateEnergyCounter();
        }
        else
        {
            Debug.LogWarning("Not enough Energy to remove.");
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
            energyCounter.text = ((int)Energy).ToString();
        }
        else 
        {
            Debug.Log("Energy counter not found");
        }
    }

    private void SaveScrap()
    {
        ES3.Save("Scrap", Scrap);
    }

    public void SaveEnergy()
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
            Energy = ES3.Load<float>("Energy");
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
