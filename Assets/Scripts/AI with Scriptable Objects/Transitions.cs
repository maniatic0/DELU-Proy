using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Transitions
{
    /// <summary> Decision to take in this transition </summary>
    public Decisions decision;
    /// <summary> State to transition to if the decision is true </summary>
    public States trueState;
    /// <summary> State to transition to if the decision is false </summary>
    public States falseState;
}
