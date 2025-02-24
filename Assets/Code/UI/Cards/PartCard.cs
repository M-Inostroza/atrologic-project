using TMPro;
using UnityEngine;

public class PartCard : MonoBehaviour
{
    [SerializeField]
    private TMP_Text cardName;

    PartData partData;

    public void SetPart(PartData newData)
    {
        partData = newData;
        cardName.text = partData.partName;
    }

    public void Deploy()
    {
        Vector3 deployPosition = new Vector3(12.78f, -6.61f, 0f);
        Instantiate(partData.partPrefab, deployPosition, Quaternion.identity);
    }
}
