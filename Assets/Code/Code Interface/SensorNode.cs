[System.Serializable]
public class SensorNode : BaseNode
{
    public override string NodeType => "Sensor";
    public float angle; // or reference to sensor reading
}

[System.Serializable]
public class IfNode : BaseNode
{
    public override string NodeType => "If";
    // store references or node IDs for condition blocks, e.g., logic operators
}

[System.Serializable]
public class LogicNode : BaseNode
{
    public override string NodeType => "Logic";
    public string operatorType; // "LessThan", "GreaterThan", etc.
}

[System.Serializable]
public class NumberNode : BaseNode
{
    public override string NodeType => "Number";
    public float value;
}

[System.Serializable]
public class ExeNode : BaseNode
{
    public override string NodeType => "Exe";
    // e.g., a function reference or action ID
}
