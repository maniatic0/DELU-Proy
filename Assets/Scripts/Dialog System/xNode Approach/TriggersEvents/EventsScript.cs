using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsScript : MonoBehaviour
{
    public static EventsScript events;

    private void Awake()
    {
        events = this;    
    }

    /// <summary> Agrega a los eventos de dialogo un evento asociado a eventName </summary>
    /// <param name="eventName">evento a agregar</param>
    public void ProcessEventString(string eventName)
    {
        if (eventName == "Test_Event1")
        {
            TriggersManager.onDialogEnd += TestEvent;
            return;
        }
        Debug.LogError("No existe el evento de nombre: " + eventName);
    }

    public void TestEvent()
    {
        Debug.Log("Evento de test1");
    }
}
