using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystemNode : MonoBehaviour
{
    /// <summary> GameObject asociado a dialogBox </summary>
    [SerializeField]
    private GameObject parentObject;

    /// <summary> Texto asociado al jugador </summary>
    [SerializeField]
    private Text playerText;
    /// <summary> Retrato del jugador </summary>
    [SerializeField]
    private Image playerPortrait;
    /// <summary> Texto asociado al no jugador </summary>
    [SerializeField]
    private Text otherText;
    /// <summary> Retrato asociado al no jugador </summary>
    [SerializeField]
    private Image otherPortrait;
    /// <summary>
    /// Texto asociado a la pregunta/eleccion que hacer
    /// </summary>
    [SerializeField]
    private Text mainChoiceText;

    /// <summary> Texto asociado a las decisiones que se pueden tomar </summary>
    /// <remarks> Maximo de 4 preguntas </remarks>
    [SerializeField]
    private Text[] choicesText = new Text[4];

    /// <summary> Velocidad de typeo de las palabras </summary>
    [SerializeField]
    private float typeSpeed = 0.1f;
    /// <summary> Indica si se esta en un dialogo </summary>
    private bool inDialog = false;
    /// <summary> Indica si se esta escribiendo algo </summary>
    private bool writing = false;
    /// <summary> Indica si se esta eligiendo </summary>
    private bool choosing = false;
    /// <summary> Indica si ya se puede pasar al proximo dialogo </summary>
    private bool readyForNext = false;
    /// <summary> Indica si se quiere saltar el typeo </summary>
    private bool allText = false;

    /// <summary> Numero de elecciones del multi nodo </summary>
    private int choicesNumber;
    /// <summary> Eleccion seleccionada </summary>
    private int actualChoice = 0;

    /// <summary> Nodo actual del grafo </summary>
    [SerializeField] private DialogBaseNode currentNode;
    /// <summary> Grafo del dialogo </summary>
    [SerializeField] private DialogGraph dialogGraph;

    private void Start()
    {
        currentNode = dialogGraph.start;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !inDialog) //&& !inDialog)
        {
            StartDialog();
        }
        else if (Input.GetKeyDown(KeyCode.E) && inDialog && !writing && readyForNext)
        {
            MoveToNext();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && choosing)
        {   
            if (actualChoice < choicesNumber - 1)
            {
                actualChoice += 1;
            }
            else
            {
                actualChoice = 0;
            }
            Debug.Log("Opcion seleccionada: " + actualChoice);
        }
        if (Input.GetKeyDown(KeyCode.Return) && choosing)
        {
            MoveToNext();
        }
    }

    /// <summary> Inicia el dialogo</summary>
    void StartDialog()
    {
        inDialog = true;
        currentNode = dialogGraph.start;
        parentObject.SetActive(true);
        ProcessNode(currentNode);
    }

    void EndDialog()
    {

    }

    /// <summary> Procesa el nodo segun su tipo para iniciar el siguiente dialogo </summary>
    /// <param name="node">Nodo a procesar</param>
    void ProcessNode(DialogBaseNode node)
    {
        //Debug.Log(node.GetType());
        if (node.GetType() == typeof(DialogNode)) {
            //Debug.Log("Node de tipo dialogo!");
            ProcessDialogNode((DialogNode)node);
        }
        else if (node.GetType() == typeof(MultiNode))
        {
            //Debug.Log("Node de tipo Multi!");
            ProcessMultiNode(node as MultiNode);
        }
    }

    IEnumerator DebugType(DialogBaseNode node)
    {
        while (node == null)
        {
            Debug.Log("Empty");
            yield return new WaitForSeconds(1f);
        }
        while (node != null)
        {
            ProcessNode(node);
            yield return new WaitForSeconds(1f);
        }
    }

    /// <summary> Procesa un nodo de dialogo </summary>
    /// <param name="node">Nodo de dialogo</param>
    void ProcessDialogNode(DialogNode node)
    {
        //QUE HACER CON ESTO
        StartCoroutine(TypeDialog(node));
    }

    /// <summary> Procesa un nodo multi opciones </summary>
    /// <param name="node">Nodo multi opciones</param>
    void ProcessMultiNode(MultiNode node)
    {
        //QUE HACER COSO
        actualChoice = 0;
        StartCoroutine(TypeMultiNode(node));
    }

    /// <summary> Cambia al siguiente nodo del dialogo </summary>
    void MoveToNext()
    {
        //Debug.Log(currentNode.GetType());
        if (currentNode.GetType() != typeof(MultiNode))
        {
            currentNode.NextNode();
        }
        else if (currentNode.GetType() == typeof(MultiNode))
        {
            (currentNode as MultiNode).ChooseOption(actualChoice);
        }
        currentNode = dialogGraph.current;
        if (currentNode != null)
        {
            ProcessNode(currentNode);
            return;
        }
        Debug.Log("Finalizado dialogo!");
        DisableUI();
        //Stop

    }
    #region UI
    //Importante tener los GameObjects asociados activados, esto solo desactiva los componentes.

    /// <summary> Activa UI de dialogo del jugador </summary>
    void EnablePlayer()
    {
        playerText.enabled = true;
        playerPortrait.enabled = true;
    }

    /// <summary> Desactiva UI de dialogo del jugador </summary>
    void DisablePlayer()
    {
        playerText.enabled = false;
        playerPortrait.enabled = false;
    }

    /// <summary> Activa UI de dialogo del otro </summary>
    void EnableOther()
    {
        otherText.enabled = true;
        otherPortrait.enabled = true;
    }

    /// <summary> Desactiva UI de dialogo del otro </summary>
    void DisableOther()
    {
        otherText.enabled = false;
        otherPortrait.enabled = false;
    }

    void EnableChoices()
    {
        mainChoiceText.enabled = true;
        playerPortrait.enabled = true;
    }

    void DisableChoices()
    {
        mainChoiceText.enabled = false;
        playerPortrait.enabled = false;
        foreach(Text choice in choicesText)
        {
            choice.enabled = false;
        }
    }

    void EnableChoice(int choice)
    {
        choicesText[choice].enabled = true;
    }

    void DisableUI()
    {
        DisablePlayer();
        DisableOther();
        DisableChoices();
        parentObject.SetActive(false);
    }

    //void EnableMulti
    //void DisableMulti
    #endregion

    /// <summary> Typea un nodo de dialogo </summary>
    /// <param name="node">Nodo a typear</param>
    IEnumerator TypeDialog(DialogNode node)
    {
        //Debug.Log("Escribiendito");
        string diagText = node.dialog_text;
        playerText.text = "";
        otherText.text = "";

        allText = false;
        writing = true;
        DisableChoices();

        if (node.isPlayer)
        {
            DisableOther();
            EnablePlayer();
            foreach (char letter in diagText)
            {
                playerText.text += letter;
                yield return new WaitForSeconds(typeSpeed);
                if (allText)
                {
                    playerText.text = diagText;
                    break;
                }
            }
        }
        else
        {
            DisablePlayer();
            EnableOther();
            foreach (char letter in diagText)
            {
                otherText.text += letter;
                yield return new WaitForSeconds(typeSpeed);
                if (allText)
                {
                    otherText.text = diagText;
                    break;
                }
            }
        }
        writing = false;
        readyForNext = true;
        allText = true;
    }

    /// <summary> Typea un nodo multiple </summary>
    /// <param name="node">Nodo a typear con opciones</param>
    IEnumerator TypeMultiNode(MultiNode node)
    {
        //Debug.Log("Escribiendito");
        string mainText = node.multiText;
        Debug.Log(mainText);
        List<Answer> answers = node.choices;

        playerText.text = "";
        otherText.text = "";
        mainChoiceText.text = "";

        choicesNumber = answers.Count;
        allText = false;
        writing = true;

        DisablePlayer();
        DisableOther();
        DisableChoices();
        EnableChoices();

        foreach (char letter in mainText)
        {
            mainChoiceText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
            if (allText)
            {
                mainChoiceText.text = mainText;
                break;
            }
        }
        allText = false;

        int index = 0;
        foreach(Answer answer in answers)
        {
            string choiceText = answer.choiceText;
            choicesText[index].text = "";
            EnableChoice(index);          
            //Debug.Log(choiceText);
            foreach (char letter in choiceText)
            {
                choicesText[index].text += letter;
                yield return new WaitForSeconds(typeSpeed);
                if (allText)
                {
                    choicesText[index].text += letter;
                    break;
                }
            }
            index += 1;
        }
        allText = false;
        writing = false;
        choosing = true;
    }
}
