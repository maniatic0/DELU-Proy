using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XNode;

public abstract class DialogBaseNode : Node {
    [Input(backingValue = ShowBackingValue.Never, typeConstraint = TypeConstraint.Inherited)] public DialogBaseNode input;
    [Output(backingValue = ShowBackingValue.Never)] public DialogBaseNode output;

    public virtual void NextNode()
    {      
        DialogGraph dGraph = graph as DialogGraph;
        dGraph.current = GetOutputPort("output").node as DialogBaseNode;
    }

    protected virtual void Init()
    {
        base.Init();
    }


    public void GoToNode(DialogBaseNode nextNode)
    {
        DialogGraph dGraph = graph as DialogGraph;
        dGraph.current = nextNode;
    }

    // Return the correct value of an output port when requested
    public override object GetValue(NodePort port)
    {
        return null; // Replace this
    }
}