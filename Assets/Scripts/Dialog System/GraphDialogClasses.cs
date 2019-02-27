using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphDialog
{
    public Dictionary<int, DialogNodes> _nodes = new Dictionary<int, DialogNodes>();
}

[System.Serializable]
public class DialogNodes
{
    public string dialog_text;
    public List<int> Neighboors;
    public bool question;
}


[System.Serializable]
public class NodeList
{
    public List<DialogNodes> Diags_Nodes = new List<DialogNodes>();
}
