using UnityEngine;

public abstract class Decisions : ScriptableObject
{
    /// <summary> Checks decision sucess </summary>
    /// <param name="controller"> State controller </param>
    /// <returns> If the decision is succesfull</returns>
    public abstract bool Decide(StateController controller);
}
