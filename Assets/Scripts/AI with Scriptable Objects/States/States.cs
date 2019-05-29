using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/State")]
public class States : ScriptableObject
{
    /// <summary> State actions </summary>
    public Actions[] actions;
    /// <summary> State transitions </summary>
    public Transitions[] transitions;
    public Color debugColor = Color.gray;

    /// <summary> Update state status </summary>
    /// <param name="controller"> State controller </param>
    public void UpdateState(StateController controller)
    {
        DoActions(controller);
        CheckTransitions(controller);
    }

    /// <summary> Execute state actions </summary>
    /// <param name="controller"> State controller </param>
    private void DoActions(StateController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(controller);
        }
    }

    /// <summary> Check for valid transitions in the state </summary>
    /// <param name="controller"> State controller </param>
    private void CheckTransitions(StateController controller)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            bool isSucess = transitions[i].decision.Decide(controller);
            if (isSucess)
            {
                //Maybe return here
                controller.ChangeState(transitions[i].trueState);
            }
            else
            {
                controller.ChangeState(transitions[i].falseState);
            }
        }
    }

}
