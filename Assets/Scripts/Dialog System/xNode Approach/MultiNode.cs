using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XNode;

[NodeTint("#B349DA")]
public class MultiNode : DialogBaseNode
{
    [TextArea] public string multiText;

    [Output(instancePortList = true)] public List<Answer> choices = new List<Answer>();

    public void ChooseOption(int choice)
    {
        Debug.Log("Opcion: " + choice + " elegida!");
        Debug.Log(choices.Count);
        NodePort port = GetOutputPort("choices " + choice);
        DialogBaseNode node = port.GetConnection(0).node as DialogBaseNode;
        (graph as DialogGraph).SetCurrent(node);
    }
}

[System.Serializable]
public class Answer
{
    public string choiceText = "Jeje";
}