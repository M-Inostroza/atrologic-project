using UnityEngine;
using UnityEngine.UI;

public class PartCard : MonoBehaviour
{
    [SerializeField]
    private Text partNameText;
    [SerializeField]
    private Text partTypeText;
    [SerializeField]
    private Image partIconImage;
    [SerializeField]
    private Button attachButton;
    [SerializeField]
    private Button detachButton;

    private Part part;

    public void Initialize(Part part)
    {
        this.part = part;
        partNameText.text = part.name;
        partTypeText.text = part.partType.ToString();
        partIconImage.sprite = part.GetComponent<SpriteRenderer>().sprite;

        attachButton.onClick.AddListener(OnAttachButtonClicked);
        detachButton.onClick.AddListener(OnDetachButtonClicked);
    }

    private void OnAttachButtonClicked()
    {
        // Logic to attach the part to the robot
        // Example: RobotManager.Instance.AttachPartToRobot(part, attachmentPointIndex);
    }

    private void OnDetachButtonClicked()
    {
        // Logic to detach the part from the robot
        // Example: RobotManager.Instance.DetachPartFromRobot(part);
    }
}