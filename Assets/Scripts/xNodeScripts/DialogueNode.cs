using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class DialogueNode : BaseNode {

    [Input] public int entry;
    public Sprite background;

    public Sprite rightPersonSprite;
    public string rightPersonText;
    public bool addAdditionalRightPersonText;
    public string additionalRightPersonText;

    public bool addLeftPerson;

    public Sprite leftPersonSprite;
    public string leftPersonText;
    public bool addAdditionalLeftPersonText;
    public string additionalLeftPersonText;

    public string button_1_Text;
    public string button_2_Text;
    public string button_3_Text;

    [Output] public int option_1;
    [Output] public int option_2;
    [Output] public int option_3;


    public override NodeType GetNodeType()
    {
        return NodeType.DIALOGUE_NODE;
    }

    public override string GetButton_1_Text()
    {
        return button_1_Text;
    }

    public override string GetButton_2_Text()
    {
        return button_2_Text;
    }

    public override string GetButton_3_Text()
    {
        return button_3_Text;
    }


}