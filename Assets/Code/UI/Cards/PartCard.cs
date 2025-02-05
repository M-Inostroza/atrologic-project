using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PartCard : MonoBehaviour
{
    [SerializeField] private Part part;

    //instantiate a new part
    public void InstantiatePart()
    { 
        Part newPart = Instantiate(part);
        newPart.transform.position = new Vector3(12.8f, -7f, 0f);
    }
}