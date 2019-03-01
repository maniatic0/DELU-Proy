using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///<sumarry>
///Script que se encarga de controlar la aparición de los diálogos con un NPC.
/// </sumarry>
/// 

//TODO
//Preguntar como vamos a manejar los archivos de texto, por strings/paths
//O con una variable publica que contenga el archivo


//Importante!
//En StartDiag() se debería cancelar cualquier otro input del personaje.
//En NextDiag(node), si el nodo es -1, se debe volver a recuperar el control del personaje.
public class GraphDialogSystem : MonoBehaviour
{
    NodeList nodeList = new NodeList();
    GraphDialog dialogsGraph = new GraphDialog();

    public GameObject diagBox;
    public Text mainText;
    public Text questionText;
    public Text[] answers = new Text[4];
    public float typeSpeed = 0.1f;

    bool writing = false;
    bool allText = false;
    bool readyNext = false;
    bool choosing = false;
    int nextDiag = 0;
    int questionSelec = 0;
    List<int> actualQuestions;

    void Awake()
    {
        //Desactivando la UI al iniciar hasta que haya un dialogo
        DisableUI();
    }
    // Start is called before the first frame update
    void Start()
    {
        ReadFile("Dialogs/test2");
        CreateGraph(nodeList.Diags_Nodes, dialogsGraph);
        StartDiag();
    }

    // Update is called once per frame
    void Update()
    {
        //Input para pasar al siguiente diálogo
        if (readyNext && Input.GetKeyDown(key: KeyCode.Return))
        {
            NextDiag(node: nextDiag);
            //Quitar la flechita
        }

        //Input para adelantar la escritura de texto
        if (!readyNext && writing && Input.GetKeyDown(key: KeyCode.Space))
        {
            allText = true;
            writing = false;
            //Hacer aparecer la flechita
        }

        //Bloque para alternar entes las opciones de diálogo y elegir una opción
        if (choosing && Input.GetKeyDown(KeyCode.DownArrow))
        {
            answers[questionSelec].gameObject.transform.Find("QuestionChoicer").gameObject.SetActive(false);
            //Mover la flechita
            if (questionSelec < actualQuestions.Count - 1)
            {
                questionSelec += 1;
            }
            else
            {
                questionSelec = 0;
            }
            answers[questionSelec].gameObject.transform.Find("QuestionChoicer").gameObject.SetActive(true);
            print(questionSelec);
        }
        else if (choosing && Input.GetKeyDown(key: KeyCode.UpArrow))
        {
            answers[questionSelec].gameObject.transform.Find("QuestionChoicer").gameObject.SetActive(false);
            //Mover la flechita   
            if (questionSelec > 0)
            {
                questionSelec -= 1;
            }
            else
            {
                questionSelec = actualQuestions.Count - 1;
            }
            answers[questionSelec].gameObject.transform.Find("QuestionChoicer").gameObject.SetActive(true);
            print(questionSelec);
        }
        else if (choosing && Input.GetKeyDown(key: KeyCode.Return))
        {
            //Quitar la flechita
            NextDiag(node: actualQuestions[index: questionSelec]);//dialogsGraph._nodes[actualQuestions[questionSelec]].Neighboors[0]);
        }
    }

    /// <summary>
    /// Solo inicia la conversación.
    /// </summary>
    public void StartDiag()
    {
        //Activando todo lo que tenga que ver con la UI
        diagBox.SetActive(true);
        mainText.enabled = true;
        questionText.enabled = true;
        foreach (Text text in answers)
        {
            text.gameObject.transform.Find("QuestionChoicer").gameObject.SetActive(false);
            text.text = "";
            text.enabled = true;
        }
        NextDiag(0);
        //Hacer aca que se active la cajita de texto.
    }

    /// <summary>
    /// Función que se encarga de la aparición de texto en pantalla
    /// y la selección de múltiples diálogos posibles.
    /// </summary>
    /// <param name="node">Nodo del cual se obtendrá información del diálogo</param>
    /// <returns>Solo una corutina</returns>
    IEnumerator Type(int node)
    {
        if (!dialogsGraph._nodes[node].question)
        {
            writing = true;
            nextDiag = dialogsGraph._nodes[node].Neighboors[0];
            foreach (char letter in dialogsGraph._nodes[node].dialog_text)
            {
                mainText.text += letter;
                
                //Detecta si se quiere saltar directamente al diálogo
                //y lo muestra todo de una
                if (allText)
                {
                    mainText.text = dialogsGraph._nodes[node].dialog_text;
                    break;
                }

                yield return new WaitForSeconds(typeSpeed);
            }
            writing = false;
            allText = false;
            readyNext = true;
            //Hacer que aca salga una flechita para indicar que termino el dialogo
        }
        else
        {
            StartCoroutine(Question(dialogsGraph._nodes[node].Neighboors, node));
        }
        
    }

    /// <summary>
    /// Función encargada de preparar la UI, variables para
    /// el siguiente diálogo y de la salida de una conversación.
    /// </summary>
    /// <param name="node">Nodo al que se accederá a continuación</param>
    public void NextDiag(int node)
    {
        mainText.text = "";
        questionText.text = "";
        foreach (Text text in answers)
        {
            text.gameObject.transform.Find("QuestionChoicer").gameObject.SetActive(false);
            text.text = "";
        }

        readyNext = false;
        choosing = false;
        allText = false;

        if (node != -1)
        {
            StartCoroutine(Type(node));
        }
        else
        {
            StopAllCoroutines();
            DisableUI();
        }
    }

    /// <summary>
    /// Función encargada de la aparición de las posibles opciones de diálogo
    /// y de la preparación del Input para seleccionar una de susodichas opciones.
    /// </summary>
    /// <param name="neighbours">Opciones de diálogo posibles</param>
    /// <param name="actualNode">Nodo del cual se originan las múltiples opciones</param>
    /// <returns>Solo una corutina</returns>
    public IEnumerator Question(List<int> neighbours, int actualNode)
    {
        writing = true;
        actualQuestions = FetchAnswerNodes(neighbours);
        foreach (char letter in dialogsGraph._nodes[actualNode].dialog_text)
        {
            questionText.text += letter;
            if (allText)
            {
                questionText.text = dialogsGraph._nodes[actualNode].dialog_text;
                break;
            }
            yield return new WaitForSeconds(typeSpeed);
        }
        writing = false;

        int counter = 0;
        foreach (int neigh in neighbours)
        {
            foreach (char letter in dialogsGraph._nodes[neigh].dialog_text)
            {
               answers[counter].text += letter;
                yield return new WaitForSeconds(typeSpeed);
            }
            counter += 1;
        }
        answers[0].gameObject.transform.Find("QuestionChoicer").gameObject.SetActive(true);
        choosing = true;
        //Hacer que salga una flechita para seleccionar una opcion
    }

    /// <summary>
    /// Función encargada de obtener la ID (?) de los nodos a los cuales
    /// se dirige cada opción en un diálogo de selección múltiple.
    /// </summary>
    /// <param name="possibleAnswers">Lista de ID (?) de los posibles diálogos.</param>
    /// <returns>Lista que contiene los nodos a los que se dirige cada opción de diálogo.</returns>
    public List<int> FetchAnswerNodes(List<int> possibleAnswers)
    {
        List<int> answerNodes = new List<int>();
        foreach(int answer in possibleAnswers)
        {
            answerNodes.Add(dialogsGraph._nodes[answer].Neighboors[0]);
        }
        return answerNodes;
    }

    /// <summary>
    /// Función encargada de recibir todos los datos de un JSON que contiene
    /// los diálogos.
    /// </summary>
    /// <param name="sceneName">Ruta del archivo JSON a abrir</param>
    public void ReadFile(string sceneName)
    {
        TextAsset asset = Resources.Load(sceneName) as TextAsset;
        if (asset != null)
        {
            nodeList = JsonUtility.FromJson<NodeList>(asset.text);
        }
    }

    /// <summary>
    /// Función encargada de crear un grafo que contendrá los nodos
    /// de diálogo.
    /// </summary>
    /// <param name="nodes">Nodos de diálogo</param>
    /// <param name="graph">Grafo a modificar</param>
    public void CreateGraph(List<DialogNodes> nodes, GraphDialog graph)
    {
        for (int node = 0; node < nodes.Count; node++)
        {
            graph._nodes.Add(node, nodes[node]);
        }
    }
    //Debería hacer una función para resetear el grafo

    public void DisableUI()
    {
        diagBox.SetActive(false);
        mainText.enabled = false;
        questionText.enabled = false;
        foreach (Text text in answers)
        {
            text.gameObject.transform.Find("QuestionChoicer").gameObject.SetActive(false);
            text.enabled = false;
        }
    }
}
