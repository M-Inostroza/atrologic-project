using TMPro;
using UnityEngine;

public class PartCard : MonoBehaviour
{
    [SerializeField]
    private TMP_Text cardName;

    [SerializeField] private UnityEngine.UI.Button deployButton;
    [SerializeField] private UnityEngine.UI.Button modifyButton;

    PartData partData;

    public void SetPart(PartData newData)
    {
        partData = newData;
        cardName.text = partData.partName;

        // 🔹 Disable Deploy button if the part is already deployed
        deployButton.interactable = !partData.isDeployed;
    }

    public void Deploy()
    {
        Debug.Log("From Deploy");
        if (partData == null)
        {
            Debug.LogError("Cannot deploy: PartData is null!");
            return;
        }

        Vector3 deployPosition = new Vector3(12.78f, -6.61f, 0f);

        // 🔹 Load the prefab from Resources based on part name
        GameObject partPrefab = Resources.Load<GameObject>($"Parts/{partData.partName}");

        if (partPrefab != null)
        {
            Instantiate(partPrefab, deployPosition, Quaternion.identity);
            Debug.Log($"Deployed {partData.partName} at {deployPosition}");
        }
        else
        {
            Debug.LogError($"Part prefab '{partData.partName}' not found in Resources!");
        }
    }

    public void ModifyDeployedPart()
    {
        Debug.Log("From modify");
        if (partData.isDeployed)
        {
            // 🔹 Find the deployed part in the scene
            GameObject deployedPart = GameObject.Find(partData.partName);

            if (deployedPart != null)
            {
                deployedPart.GetComponent<SpriteRenderer>().color = Color.red;
                Debug.Log($"Modified {partData.partName}");
            }
            else
            {
                Debug.LogWarning("No deployed part found!");
            }
        }
    }
}
