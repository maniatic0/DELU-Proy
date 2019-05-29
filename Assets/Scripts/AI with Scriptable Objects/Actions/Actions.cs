using UnityEngine;

public abstract class Actions : ScriptableObject
{
    /// <summary> Execute this action </summary>
    /// <param name="controller"> State controller </param>
    public abstract void Act(StateController controller);
}
