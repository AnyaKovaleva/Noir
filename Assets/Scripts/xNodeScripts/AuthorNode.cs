using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class AuthorNode : BaseNode {

    [Input] public int entry;

    public Sprite background;
    [TextArea]
    public string authorText;
    public string button_1_Text;
    public string button_2_Text;
    public string button_3_Text;

    [Output] public int option_1;
    [Output] public int option_2;
    [Output] public int option_3;

    public override NodeType GetNodeType()
    {
        return NodeType.AUTHOR_NODE;
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

    // Use this for initialization
    protected override void Init() {
		base.Init();
		
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		return null; // Replace this
	}
}