using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateAssetMenu]
public class DialogGraph : NodeGraph {
    public DialogBaseNode current;
    public DialogBaseNode start;

    public void SetCurrent(DialogBaseNode node)
    {
        current = node;
    }

    public void SetStart(DialogBaseNode node)
    {
        start = node;
        current = node;
    }
}