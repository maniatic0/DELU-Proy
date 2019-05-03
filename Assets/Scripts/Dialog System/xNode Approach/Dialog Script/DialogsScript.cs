using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogsScript : MonoBehaviour
{
    //Explicacion de pre/post grafo.
    //En caso de que haya un dialogo unico que no se quiera repetir, sin reemplazar por un
    //dialogo distinto que se repita, ej: Ya te dije lo que tenia que decirte!. El pre grafo
    //es el dialogo que no se repetira y post grafo el que se empezara a repetir cuando se trate
    //de volver a interactuar. En caso de que un dialogo que si se pueda repetir, siempre se accedera
    //al pre grafo (el post puede ser null en este caso)

    /// <summary> Path del TextAsset correspondiente a la situacion </summary>
    public DialogGraph DialogFile
    {
        get { return AdecuateGraph(); }
    }
       
    /// <summary> Grafo del primer dialogo </summary>
    [SerializeField]
    private DialogGraph preGraph = null;

    /// <summary> Grafo del segundo dialogo </summary>
    [SerializeField]
    private DialogGraph postGraph = null;

    /// <summary> Indica si el dialogo es repetible </summary>
    [SerializeField]
    private bool repeteable = true;

    /// <summary> Indica si ya el dialogo ocurrio </summary>
    private bool alreadyDone = false;

    /// <summary> Devuelve el grafo asociado al dialogo correspondiente </summary>
    /// <returns>Graph de dialogo que corresponde</returns>
    DialogGraph AdecuateGraph()
    {
        if (repeteable)
        {
            return preGraph;
        }
        else if (!repeteable && alreadyDone)
        {
            return postGraph;
        }
        else if (!repeteable && !alreadyDone)
        {
            return preGraph;
        }
        return null;
    }

    /// <summary> Sirve para cambiar el grafo de alguno de los dialogos </summary>
    /// <param name="post">Indica si se quiere cambiar el post</param>
    /// <param name="graph">Path del nuevo archivo de dialogo</param>
    public void ChangeDialogPath(bool post, DialogGraph graph)
    {
        if (post)
        {
            postGraph = graph;
        }
        else
        {
            preGraph = graph;
        }
    }

    /// <summary> Cambia el estado de alreadyDone si es falso </summary>
    public void ChangeStatus()
    {
        if (!alreadyDone)
        {
            alreadyDone = true;
        }
    }
}
