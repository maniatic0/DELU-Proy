using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Test Action2")]
public class TestAction2 : Actions
{
    public override void Act(StateController controller)
    {
        Debug.Log("Action2");
    }
}
