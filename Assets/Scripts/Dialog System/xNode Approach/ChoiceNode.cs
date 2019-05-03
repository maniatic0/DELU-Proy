using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using XNode;

public class ChoiceNode : DialogBaseNode {

    public string eventName = "";

    protected override void Init()
    {
        base.Init();

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