using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Test Decision2")]
public class TestDecision2 : Decisions
{
    public override bool Decide(StateController controller)
    {
        return false;
    }
}