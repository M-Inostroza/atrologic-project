using TMPro;
using UnityEngine;

public class LevelUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scrapCounter;

    public void UpdateScrapCounter(int currentScrap)
    {
        scrapCounter.text = currentScrap.ToString();
    }
}
