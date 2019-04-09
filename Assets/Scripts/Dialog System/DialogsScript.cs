using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogsScript : MonoBehaviour
{
    //Explicacion de pre/post path.
    //En caso de que haya un dialogo unico que no se quiera repetir, sin reemplazar por un
    //dialogo distinto que se repita, ej: Ya te dije lo que tenia que decirte!. El pre path
    //es el dialogo que no se repetira y post path el que se empezara a repetir cuando se trate
    //de volver a interactuar. En caso de que un dialogo si se pueda repetir, siempre se accedera
    //al pre path (el post puede ser en este caso)

    /// <summary>
    /// Path del TextAsset correspondiente a la situacion
    /// </summary>
    public string DialogFile
    {
        get { return AdecuatePath(); }
    }
       
    /// <summary>
    /// Path del TextAsset del primer dialogo
    /// </summary>
    [SerializeField]
    private string prePath = "Dialogs/nonRepeatable";

    /// <summary>
    /// Path del TextAsset del segundo dialogo
    /// </summary>
    [SerializeField]
    private string postPath = "Dialogs/repeatableTest";

    /// <summary>
    /// Indica si el dialogo es repetible
    /// </summary>
    [SerializeField]
    private bool repeteable = true;

    /// <summary>
    /// Indica si ya el dialogo ocurrio
    /// </summary>
    private bool alreadyDone = false;

    /// <summary>
    /// Devuelve el path asociado al dialogo correspondiente
    /// </summary>
    /// <returns>Path del TextAsset correspondiente</returns>
    string AdecuatePath()
    {
        if (repeteable)
        {
            return prePath;
        }
        else if (!repeteable && alreadyDone)
        {
            return postPath;
        }
        else if (!repeteable && !alreadyDone)
        {
            return prePath;
        }
        return null;
    }

    /// <summary>
    /// Sirve para cambiar el path de alguno de los dialogos
    /// </summary>
    /// <param name="post">Indica si se quiere cambiar el post</param>
    /// <param name="path">Path del nuevo archivo de dialogo</param>
    public void ChangeDialogPath(bool post, string path)
    {
        if (post)
        {
            postPath = path;
        }
        else
        {
            prePath = path;
        }
    }

    /// <summary>
    /// Cambia el estado de alreadyDone si es falso.
    /// </summary>
    public void ChangeStatus()
    {
        if (!alreadyDone)
        {
            alreadyDone = true;
        }
    }
}
