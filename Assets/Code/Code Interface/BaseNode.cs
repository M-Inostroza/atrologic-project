using UnityEngine;

[System.Serializable]
public abstract class BaseNode
{
    public string nodeID;
    public Vector2 position;
    // Where this node is located in the UI (optional if you want to store layout)

    // Connections: references to other nodes
    // E.g., public List<string> inputNodeIDs; // node IDs for inputs
    // public List<string> outputNodeIDs; // node IDs for outputs

    // Optional: Node type or enum
    public abstract string NodeType { get; }
}
