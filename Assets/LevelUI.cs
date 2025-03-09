using TMPro;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    [SerializeField] TMP_Text angleDegText;

    private void OnEnable()
    {
        Sensor.OnAngleDetected += UpdateAngleText;
    }

    private void OnDisable()
    {
        Sensor.OnAngleDetected -= UpdateAngleText;
    }

    private void UpdateAngleText(float angle)
    {
        angleDegText.text = $"{angle.ToString("F2")}°";
    }
}
