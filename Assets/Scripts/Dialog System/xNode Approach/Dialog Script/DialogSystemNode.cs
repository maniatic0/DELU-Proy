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
    /// <summary> Texto asociado a la pregunta/eleccion que hacer </summary>
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
    protected bool inDialog = false;
    /// <summary> Indica si se esta escribiendo algo </summary>
    protected bool writing = false;
    /// <summary> Indica si se esta eligiendo </summary>
    protected bool choosing = false;
    /// <summary> Indica si ya se puede pasar al proximo dialogo </summary>
    protected bool readyForNext = false;
    /// <summary> Indica si se quiere saltar el typeo </summary>
    protected bool allText = false;

    /// <summary> Numero de elecciones del multi nodo </summary>
    protected int choicesNumber;
    /// <summary> Eleccion seleccionada </summary>
    protected int actualChoice = 0;

    /// <summary> Nodo actual del grafo </summary>
    [SerializeField] private DialogBaseNode currentNode;
    /// <summary> Grafo del dialogo </summary>
    [SerializeField] private DialogGraph dialogGraph;

    private void Start()
    {
        parentObject.SetActive(false);
    }

    /// <summary> Inicia el dialogo </summary>
    public virtual void StartDialog(DialogGraph graph)
    {
        inDialog = true;
        dialogGraph = graph;
        currentNode = dialogGraph.start;
        parentObject.SetActive(true);
        ProcessNode(currentNode);
    }

    /// <summary> Finaliza el dialogo </summary>
    public virtual void EndDialog()
    {
        //Eventos de dialogo
        TriggersManager.RaiseOnDialogEnd();
        writing = inDialog = allText = choosing = readyForNext = false;
        actualChoice = choicesNumber = 0;
        //Se tienen que parar todas las corutinas de typeo para evitar que si se vuelve a abrir un dialogo
        //muy rapido, escriban dos corutinas writing al mismo tiempo
        StopAllCoroutines();
    }

    /// <summary> Procesa el nodo segun su tipo para iniciar el siguiente dialogo </summary>
    /// <param name="node">Nodo a procesar</param>
    void ProcessNode(DialogBaseNode node)
    {
        ////Debug.Log(node.GetType());
        if (node.GetType() == typeof(DialogNode)) {
            ////Debug.Log("Node de tipo dialogo!");
            ProcessDialogNode((DialogNode)node);
        }
        else if (node.GetType() == typeof(MultiNode))
        {
            ////Debug.Log("Node de tipo Multi!");
            ProcessMultiNode(node as MultiNode);
        }
        else if (node.GetType() == typeof(ChoiceNode))
        {
            ProcessChoiceNode(node as ChoiceNode);
        }
    }

    /// <summary> Procesa un nodo de dialogo </summary>
    /// <param name="node">Nodo de dialogo a procesar</param>
    void ProcessDialogNode(DialogNode node)
    {
        StartCoroutine(TypeDialog(node));
    }

    /// <summary> Procesa un nodo multi opciones </summary>
    /// <param name="node">Nodo multi opciones a procesar</param>
    void ProcessMultiNode(MultiNode node)
    {
        actualChoice = 0;
        StartCoroutine(TypeMultiNode(node));
    }

    /// <summary> Procesa un nodo choice </summary>
    /// <param name="node">Nodo choice a procesar</param>
    void ProcessChoiceNode(ChoiceNode node)
    {
        if (node.eventName == null || node.eventName == "")
        {
            Debug.LogError("Choice node can't have null or empty name string!");
            return;
        }
        EventsScript.events.ProcessEventString(node.eventName);
        MoveToNext();
    }

    /// <summary> Cambia al siguiente nodo del dialogo </summary>
    public void MoveToNext()
    {
        if (currentNode.GetType() != typeof(MultiNode))
        {
            currentNode.NextNode();
        }
        else if (currentNode.GetType() == typeof(MultiNode))
        {
            (currentNode as MultiNode).ChooseOption(actualChoice);
            choosing = false;
        }

        currentNode = dialogGraph.current;

        if (currentNode != null)
        {
            ProcessNode(currentNode);
            return;
        }
        //Debug.Log("Finalizado dialogo!");
        DisableUI();
        EndDialog();
    }

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

    /// <summary> Activa UI de multi nodo </summary>
    void EnableChoices()
    {
        mainChoiceText.enabled = true;
        playerPortrait.enabled = true;
    }

    /// <summary> Desactiva UI de multi nodo </summary>
    void DisableChoices()
    {
        mainChoiceText.enabled = false;
        playerPortrait.enabled = false;
        foreach(Text choice in choicesText)
        {
            choice.enabled = false;
        }
    }

    public void EnableChoicePointer(int choice)
    {
        //Debug.Log("asd");
        choicesText[choice].transform.Find("QuestionChoicer").GetComponent<Image>().enabled = true;
    }

    public void DisableChoicePointer(int choice)
    {
        //Debug.Log("bye");
        choicesText[choice].transform.Find("QuestionChoicer").GetComponent<Image>().enabled = false;
    }

    /// <summary> Activa UI de pregunta de index choice en choicesText </summary>
    /// <param name="choice">Pregunta a activar UI</param>
    void EnableChoice(int choice)
    {
        choicesText[choice].enabled = true;
    }

    /// <summary>
    /// Desactiva UI del sistema de dialogo junto a su gameObject padre </summary>
    void DisableUI()
    {
        DisablePlayer();
        DisableOther();
        DisableChoices();
        parentObject.SetActive(false);
    }

    /// <summary> Corutina encarga de escribir el dialogo letra por letra </summary>
    /// <param name="textBox">Caja de texto a modificar</param>
    /// <param name="dialog">String del dialogo a escribir</param>
    IEnumerator WriteText(Text textBox, string dialog)
    {
        foreach (char letter in dialog)
        {
            textBox.text += letter;
            yield return new WaitForSeconds(typeSpeed);
            if (allText)
            {
                textBox.text = dialog;
                break;
            }
        }
        allText = false;
    }

    //yield new StartCoroutine... sirve para pausar la ejecucion de la funcion/corutina principal
    //mientras corre la corutina writing, si esto no se hace, en el caso de las preguntas se escribiran
    //todas al mismo tiempo junto a la pregunta. Tambien se evita que al tratar de que salga todo el texto
    //de un dialogo, tambien se salte al siguiente.

    /// <summary> Typea un nodo de dialogo </summary>
    /// <param name="node">Nodo a typear</param>
    IEnumerator TypeDialog(DialogNode node)
    {
        string diagText = node.dialog_text;
        playerText.text = "";
        otherText.text = "";

        allText = false;
        writing = true;
        readyForNext = false;
        DisableChoices();

        if (node.isPlayer)
        {
            DisableOther();
            EnablePlayer();
            yield return StartCoroutine(WriteText(playerText, diagText));
        }
        else
        {
            DisablePlayer();
            EnableOther();
            yield return StartCoroutine(WriteText(otherText, diagText));
        }
        writing = false;
        readyForNext = true;
        allText = false;
    }

    /// <summary> Typea un nodo multiple </summary>
    /// <param name="node">Nodo a typear con opciones</param>
    IEnumerator TypeMultiNode(MultiNode node)
    {
        string mainText = node.multiText;
        ////Debug.Log(mainText);
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

        //Se empieza
        yield return StartCoroutine(WriteText(mainChoiceText, mainText));
        allText = false;

        int index = 0;
        foreach(Answer answer in answers)
        {
            string choiceText = answer.choiceText;
            choicesText[index].text = "";
            EnableChoice(index);
            yield return StartCoroutine(WriteText(choicesText[index], choiceText));
            index += 1;
        }
        EnableChoicePointer(0);
        allText = false;
        writing = false;
        choosing = true;
    }
}