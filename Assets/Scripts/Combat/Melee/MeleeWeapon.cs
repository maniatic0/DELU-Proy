using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreTags;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider))]
public class MeleeWeapon : MonoBehaviour {

    /// <summary>
    /// Tiempo de recarga entre golpes del arma, tiene que ser mas largo que su animacion
    /// </summary>
    [Tooltip("Tiempo de recarga entre golpes del arma, tiene que ser mas largo que su animacion")]
    [SerializeField]
    private float reloadTime = 0.5f;

    /// <summary>
    /// Daño a aplicar
    /// </summary>
    [Tooltip("Daño a aplicar")]
    [SerializeField]
    private EffectType damageType;

    /// <summary>
    /// Info del daño a aplicar
    /// </summary>
    private EffectInfo info;

    /// <summary>
    /// Tags relacionados al patron
    /// </summary>
    private string[] targetPatternInternal;

    /// <summary>
    /// Si el arma esta recargando
    /// </summary>
    private bool reloading = false;

    /// <summary>
    /// Si el arma esta activa
    /// </summary>
    private bool activeWeapon = false;

    /// <summary>
    /// Collider asociado a esta arma
    /// </summary>
    private Collider col;

    /// <summary>
    /// Animator Trigger Parameter para marcar inicio de ataque
    /// </summary>
    [Tooltip("Animator Trigger Parameter para marcar inicio de ataque")]
    [SerializeField]
    private string animatorParamInitAttack = "InitAttack";

    /// <summary>
    /// Animator del Arma
    /// </summary>
    private Animator ani;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        info.effect = damageType;
        info.origin = this.gameObject;
        col = gameObject.GetComponent<Collider>();
        col.isTrigger = true;
        col.enabled = false;
        ani = gameObject.GetComponent<Animator>();
    }


    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        HandleContact(other);
    }

    /// <summary>
    /// Maneja el contacto con algun objeto
    /// </summary>
    /// <param name="other">Objecto con el que se choco</param>
    void HandleContact(Collider other) {
        if (activeWeapon)
        {
            LifeManager lm = other.gameObject.GetComponent<LifeManager>();
            if (lm && other.gameObject.AnyTags(targetPatternInternal))
            {
                lm.ApplyChange(info);
                //Debug.Log("Hit " + other.gameObject.ToString());
            }
        }
        
    }

    /// <summary>
    /// Tiempo en que el arma no esta activa
    /// </summary>
    /// <returns></returns>
    IEnumerator Reload() {
        //Debug.Log("Reloading");
        reloading = true;
        yield return new WaitForSeconds(reloadTime);
        reloading = false;
        //Debug.Log("Reloaded");
    }

    /// <summary>
    /// Hace el setup del arma
    /// </summary>
    /// <param name="targetTags">Tags de los posibles objetivos</param>
    public void SetupWeapon(string[] targetTags) {
        targetPatternInternal = targetTags;
        /*
        Debug.Log("Tags:");
        foreach (var tag in targetPatternInternal)
        {
            Debug.Log(tag);
        }
         */
    }

    /// <summary>
    /// Iniciar Ataque
    /// </summary>
    /// <returns>Si el ataque se incio</returns>
    public bool StartAttack() {
        if (reloading)
        {
          return false;  
        }
        activeWeapon = true;
        col.enabled = true;
        ani.SetTrigger(animatorParamInitAttack);
        //Debug.Log("StartAttack");
        StartCoroutine(Reload());
        return true;
    }

    /// <summary>
    /// Terminar Ataque
    /// </summary>
    public void EndAttack() {
        activeWeapon = false;
        col.enabled = false;
        //Debug.Log("EndAttack");
    }

    /// <summary>
    /// Gira 180 grados el contenedor del sprite
    /// </summary>
    /// <param name="isFacingRight">Si esta viendo a la derecha</param>
    public void Flip(bool isFacingRight) {
        Vector3 currRot = gameObject.transform.rotation.eulerAngles;
        if (isFacingRight &&  !Mathf.Approximately(Mathf.DeltaAngle(currRot.y, 0.0f), 0.0f))
        {
            currRot.y = 0.0f;
        } else if (!isFacingRight &&  !Mathf.Approximately(Mathf.DeltaAngle(currRot.y, 180.0f), 0.0f)) {
            currRot.y = 180.0f;
        }
        gameObject.transform.rotation = Quaternion.Euler(currRot);
    }
}