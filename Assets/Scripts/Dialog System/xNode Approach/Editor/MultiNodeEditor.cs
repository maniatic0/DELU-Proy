using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XNode;
using XNodeEditor;

[CustomNodeEditor(typeof(MultiNode))]
public class MultiNodeEditor : NodeEditor
{
    public override void OnBodyGUI()
    {
    //    serializedObject.Update();
    //
        MultiNode node = target as MultiNode;
        DialogGraph dgraph = target.graph as DialogGraph;
        
    
        if (node.choices.Count == 0)
        {
            GUILayout.BeginHorizontal();
            NodeEditorGUILayout.PortField(GUIContent.none, target.GetInputPort("input"), GUILayout.MinWidth(0));
            NodeEditorGUILayout.PortField(GUIContent.none, target.GetOutputPort("output"), GUILayout.MinWidth(0));
            GUILayout.EndHorizontal();
        }
        else
        {
            NodeEditorGUILayout.PortField(GUIContent.none, target.GetInputPort("input"));
        }
        GUILayout.Space(-25);
        GUILayout.Label("Pregunta: ");
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("multiText"), GUIContent.none);
        if (GUILayout.Button("Hacer nodo inicial"))
        {
            dgraph.SetStart(node);
        }
        //Importante, esto hace que los puertos de la lista sean DialogBase para que puedan unirse
        NodeEditorGUILayout.InstancePortList("choices", typeof(DialogBaseNode), serializedObject, NodePort.IO.Output, Node.ConnectionType.Override);
    
        serializedObject.ApplyModifiedProperties();
    }

    public override int GetWidth()
    {
        return 400;
    }
}
