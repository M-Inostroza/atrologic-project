﻿using TMPro;
using UnityEngine;

public class PartCard : MonoBehaviour
{
    [SerializeField]
    private TMP_Text cardName;

    [SerializeField] private UnityEngine.UI.Button deployButton;
    [SerializeField] private UnityEngine.UI.Button modifyButton;

    string cardID;

    PartData partData;

    public void SetPart(PartData newData)
    {
        partData = newData;
        cardName.text = partData.partName;
        cardID = partData.partID;

        deployButton.interactable = !partData.isDeployed;
    }


    public void Deploy()
    {
        if (partData == null)
        {
            Debug.LogError("Cannot deploy: PartData is null!");
            return;
        }

        Vector3 deployPosition = new Vector3(12.78f, -6.61f, 0f);

        // Load the prefab 
        GameObject partPrefab = Resources.Load<GameObject>($"Parts/{partData.partName}");

        if (partPrefab != null)
        {
            GameObject newPart = Instantiate(partPrefab, deployPosition, Quaternion.identity);
            Part newPartScript = newPart.GetComponent<Part>();
            newPartScript.SetPrefabID(partData.partID);
            partData.isDeployed = true;
            deployButton.interactable = false;
        }
        else
        {
            Debug.LogError($"Part prefab '{partData.partName}' not found in Resources!");
        }
    }

    public void ModifyDeployedPart()
    {
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

    public void DebugPart()
    {
        Debug.Log("Deployed: " + partData.isDeployed);
        Debug.Log("Point name: " + partData.attachmentPointName);
        Debug.Log("Part ID: " + partData.partID);
        Debug.Log("Part name: " + partData.partName);
        Debug.Log("Attached?: " + partData.isAtached);
    }

    public string GetIDFromCard()
    {
        return cardID;
    }

    public void Undeploy()
    {
        partData.isDeployed = false;
        deployButton.interactable = true;
    }

    public void AttachToRobot(Transform point)
    {
        partData.isAtached = true;
        partData.attachmentPointName = point.name;
    }

    public void DetachFromRobot()
    {
        partData.isAtached = false;
        partData.attachmentPointName = "";
    }
}
