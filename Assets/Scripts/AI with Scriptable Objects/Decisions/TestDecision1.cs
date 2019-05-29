using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Test Decision1")]
public class TestDecision1 : Decisions
{
    public override bool Decide(StateController controller)
    {
        return true;
    }
}
