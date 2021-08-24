using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateAssetMenu]
public class StoryGraph : NodeGraph
{
    private BaseNode current;

    public void SetCurrentNodeToStart()
    {
        foreach (BaseNode node in nodes)
        {
            if (node.GetNodeType() == NodeType.START_NODE)
            {
                current = node;
                Debug.Log(current.GetNodeType());

                break;
            }
        }
    }

    public BaseNode GetCurrentNode()
    {
        return current;
    }

    public void StartStory()
    {
        if (current.GetNodeType() == NodeType.START_NODE)
        {
            Debug.Log(current.GetNodeType());

            if (current.GetOutputPort("start").IsConnected)
            {
                current = current.GetOutputPort("start").Connection.node as BaseNode;
                Debug.Log("Started story");
                Debug.Log(current.GetNodeType());

            }
            else
            {
                current = null;
                Debug.LogError("Start node is not assigned");
            }
        }
    }

    public bool IsConnectedOption_1()
    {
        if (current.GetNodeType() == NodeType.AUTHOR_NODE || current.GetNodeType() == NodeType.DIALOGUE_NODE)
        {
            return current.GetOutputPort("option_1").IsConnected;
        }

        return false;
    }

    public bool IsConnectedOption_2()
    {
        if (current.GetNodeType() == NodeType.AUTHOR_NODE || current.GetNodeType() == NodeType.DIALOGUE_NODE)
        {
            return current.GetOutputPort("option_2").IsConnected;
        }
        return false;
    }

    public bool IsConnectedOption_3()
    {
        if (current.GetNodeType() == NodeType.AUTHOR_NODE || current.GetNodeType() == NodeType.DIALOGUE_NODE)
        {
            return current.GetOutputPort("option_3").IsConnected;
        }
        return false;
    }

    public void MoveToOption_1()
    {
        if (IsConnectedOption_1())
        {
            current = current.GetOutputPort("option_1").Connection.node as BaseNode;
            Debug.Log(current.GetNodeType());
        }
    }

    public void MoveToOption_2()
    {
        if (IsConnectedOption_2())
        {
            current = current.GetOutputPort("option_2").Connection.node as BaseNode;
        }
    }

    public void MoveToOption_3()
    {
        if (IsConnectedOption_3())
        {
            current = current.GetOutputPort("option_3").Connection.node as BaseNode;
        }
    }

}