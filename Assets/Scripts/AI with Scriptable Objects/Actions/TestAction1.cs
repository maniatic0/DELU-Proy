using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Test Action1")]
public class TestAction1 : Actions
{
    public override void Act(StateController controller)
    {
        Debug.Log("Action1");
    }
}
