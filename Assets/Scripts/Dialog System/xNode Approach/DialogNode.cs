using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeTint("#F08080")]
public class DialogNode : DialogBaseNode {

    public string dialog_text;
    public bool isPlayer = false;

	// Use this for initialization
	protected override void Init() {
		base.Init();
		
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		return null; // Replace this
	}

    public override void NextNode()
    {
        DialogGraph target = graph as DialogGraph;
        NodePort outPort = GetOutputPort("output");
        if (outPort.IsConnected)
        {
            target.SetCurrent(GetOutputPort("output").Connection.node as DialogBaseNode);
        }
        else
        {
            target.SetCurrent(null);
        }
    }
}