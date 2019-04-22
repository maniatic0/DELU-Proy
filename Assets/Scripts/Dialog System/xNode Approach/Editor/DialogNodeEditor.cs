using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using XNodeEditor;

[CustomNodeEditor(typeof(DialogBaseNode))]
public class DialogNodeEditor : NodeEditor
{
    public override void OnHeaderGUI()
    {
        base.OnHeaderGUI();
    }

    public override void OnBodyGUI()
    {
        base.OnBodyGUI();
        DialogBaseNode node = target as DialogBaseNode;
        DialogGraph graph = node.graph as DialogGraph;
        if (GUILayout.Button("Hacer nodo inicial"))
        {
            graph.SetStart(node);
        }
    }

    public override int GetWidth()
    {
        return 400;
    }
}
