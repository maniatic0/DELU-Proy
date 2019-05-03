using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggersManager : MonoBehaviour
{
    public delegate void OnDialogEnd();
    public static event OnDialogEnd onDialogEnd;

    /// <summary> Invoca todos los eventos en la lista de eventos </summary>
    public static void RaiseOnDialogEnd()
    {
        if (onDialogEnd != null)
        {
            onDialogEnd();
            //Nullificando para quitar todos los events
            onDialogEnd = null;
        }
    }
}


