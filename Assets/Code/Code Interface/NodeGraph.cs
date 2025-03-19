using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NodeGraph
{
    public List<BaseNode> nodes = new List<BaseNode>();
    // store any additional info like graph name, version, etc.
}
