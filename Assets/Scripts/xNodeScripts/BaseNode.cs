using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public enum NodeType
{
    BASE_NODE,
    START_NODE,
    DIALOGUE_NODE,
    AUTHOR_NODE,
    MINIGAME_NODE
}

public abstract class BaseNode : Node {

    public virtual NodeType GetNodeType()
    {
        return NodeType.BASE_NODE;
    }

    public virtual string GetButton_1_Text()
    {
        return null;
    }

    public virtual string GetButton_2_Text()
    {
        return null;
    }

    public virtual string GetButton_3_Text()
    {
        return null;
    }

    //public virtual string GetString()
    //   {
    //       return null;
    //   }

    //   public virtual Sprite GetSprite()
    //   {
    //       return null;
    //   }
}