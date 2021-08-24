using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class StartNode : BaseNode {

    [Output] public int start;

    public override NodeType GetNodeType()
    {
        return NodeType.START_NODE;
    }
}