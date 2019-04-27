using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreTags;

public class PlayerDialogHandler : DialogSystemNode
{
    /// <summary> Tag de personajes con los que se puede hablar </summary>
    [SerializeField]
    private string tagName = "Talkable";

    /// <summary> Info del dialogo disponible en rango </summary>
    private DialogsScript dialogInRange = null;

    /// <summary> Indica si se esta retrasando el registro de input.</summary>
    private bool delay = false;

    private PlayerMovement playerMovement;
    private PlayerMeleeCombat meleeCombat;
    //private PlayerRangedHandler rangedCombat;
    //private CombatSystemHandler combatHandler;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        meleeCombat = GetComponent<PlayerMeleeCombat>();
        //rangedCombat = GetComponent<PlayerRangedHandler>();
        //combatHandler = GetComponent<CombatSystemHandler>();
    }

    private void Update()
    {
        if (PlayerInput.Interact && !inDialog)
        {
            if (dialogInRange != null)
            {
                StartDialog(dialogInRange.DialogFile);
                dialogInRange.ChangeStatus();
            }
        }
        if (PlayerInput.NextDialog && inDialog && !writing && readyForNext)
        {
            DisableChoicePointer(actualChoice);
            MoveToNext();
        }
        else if (PlayerInput.NextDialog && inDialog && writing && !readyForNext)
        {
            allText = true;
        }

        if (PlayerInput.AxisRaw.y == -1 && !delay && choosing)
        {
            DisableChoicePointer(actualChoice);
            if (actualChoice < choicesNumber - 1)
            {
                actualChoice += 1;
            }
            else
            {
                actualChoice = 0;
            }
            EnableChoicePointer(actualChoice);
            StartCoroutine(Delay(0.2f));
            Debug.Log("Opcion seleccionada: " + actualChoice);
        }
        else if (PlayerInput.AxisRaw.y == 1 && !delay && choosing)
        {
            DisableChoicePointer(actualChoice);
            if (actualChoice > 0)
            {
                actualChoice -= 1;
            }
            else
            {
                actualChoice = choicesNumber - 1;
            }
            EnableChoicePointer(actualChoice);
            StartCoroutine(Delay(0.2f));
            Debug.Log("Opcion seleccionada: " + actualChoice);
        }
        if (PlayerInput.NextDialog && choosing)
        {
            DisableChoicePointer(actualChoice);
            MoveToNext();
        }
    }

    /// <summary> Inicia el dialogo y desactiva los otros inputs del jugador </summary>
    /// <param name="graph">Grafo de dialogo</param>
    public override void StartDialog(DialogGraph graph)
    {
        base.StartDialog(graph);
        //playerMovement.enabled = false;
        //meleeCombat.enabled = false;
        //rangedCombat.enabled = false;
        //combatHandler.enabled = false;
    }

    /// <summary> Finaliza el dialogo y activa los otros inputs del jugador </summary>
    public override void EndDialog()
    {
        base.EndDialog();
        //playerMovement.enabled = true;
        //meleeCombat.enabled = true;
        //rangedCombat.enabled = true;
        //combatHandler.enabled = true;
    }

    //En estos dos metodos se recoge la info del dialogo de un personaje que
    //este en el rango para hablar

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.HasTag(tagName))
        {
            dialogInRange = other.GetComponent<DialogsScript>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.HasTag(tagName))
        {
            dialogInRange = null;
        }
    }
    
    /// <summary> 
    /// Se cambia el valor de delay para evitar que se deje una tecla presionada al
    /// navegar por las opciones de un dialogo y que se alterne muy rapido entre estas 
    /// </summary>
    /// <param name="delayTime">Tiempo a retrasar la deteccion de input</param>
    IEnumerator Delay(float delayTime)
    {
        delay = true;
        yield return new WaitForSeconds(delayTime);
        delay = false;
    }
}
