using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public bool activeAI = true;

    public States actualState;
    public States remainState;

    void Update()
    {
        if (!activeAI) return;
        actualState.UpdateState(this);
    }

    public void ChangeState(States newState)
    {
        if (newState != remainState)
        {
            actualState = newState;
        }
    }
}
