using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class LifeEvent : UnityEvent<LifeManager> {}

[RequireComponent(typeof(LifeColorHandler))]
public class LifeManager : MonoBehaviour
{

    /// <summary>
    /// Vida Actual
    /// </summary>
    private float life;

    /// <summary>
    /// Vida Actual
    /// </summary>
    public float Life { 
        get{ 
            return life; 
            } 
        }

    /// <summary>
    /// Maxima Vida
    /// </summary>
    [Tooltip("Maxima Vida")]
    [SerializeField]
    private float maxLife = 10.0f;

    /// <summary>
    /// Maxima Vida
    /// </summary>
    public float MaxLife { 
        get{ 
            return maxLife; 
            } 
        }
        

    /// <summary>
    /// Efectos a aplicar en update
    /// </summary>
    /// <typeparam name="EffectInfo"></typeparam>
    /// <returns></returns>
    private List<EffectInfo> continueApplying = new List<EffectInfo>();

    /// <summary>
    /// Funciones para llamar cuando la cantidad de vida cambia
    /// </summary>
    [Tooltip("Funciones para llamar cuando la cantidad de vida cambia")]
    public LifeEvent onLifeChange;

    /// <summary>
    /// Funciones para llamar cuando la cantidad de vida cambia de forma positiva
    /// </summary>
    [Tooltip("Funciones para llamar cuando la cantidad de vida cambia de forma positiva")]
    public LifeEvent onLifeChangeHeal;

    /// <summary>
    /// Funciones para llamar cuando la cantidad de vida cambia de forma negativa
    /// </summary>
    [Tooltip("Funciones para llamar cuando la cantidad de vida cambia de forma negativa")]
    public LifeEvent onLifeChangeDamage;

    /// <summary>
    /// Funciones para llamar cuando muere el humanoide
    /// </summary>
    [Tooltip("Funciones para llamar cuando muere el humanoide")]
    public LifeEvent onLifeEnd;

    /// <summary>
    /// Funciones para llamar cuando revive el humanoide
    /// </summary>
    [Tooltip("Funciones para llamar cuando revive el humanoide")]
    public LifeEvent onLifeReStart;

    /// <summary>
    /// Manejador de Cambios de Color por Vida
    /// </summary>
    private LifeColorHandler lifeColor;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected virtual void Awake()
    {
        life = maxLife;
        lifeColor = GetComponent<LifeColorHandler>();
        lifeColor.SetupLifeManager(this);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    protected virtual void Update()
    {
        for (int i = continueApplying.Count - 1; i > 0; i--)
        {
            if (!continueApplying[i].Effect.ContinueApplyingChange(
                Time.deltaTime, 
                this, 
                continueApplying[i])
                ) {
                continueApplying.RemoveAt(i);
            }
            else
            {
                ChangeLife(continueApplying[i].ApplyChange(this));
            }
        }
    }

    /// <summary>
    /// Aplicar cambio a la vida
    /// </summary>
    /// <param name="effect">Cambio a Aplicar</param>
    public virtual void ApplyChange(EffectInfo effectInfo) {
        if (effectInfo.Effect.ApplyMoreThanOnce)
        {
            continueApplying.Add(effectInfo);
        }
        else {
            ChangeLife(effectInfo.ApplyChange(this));
        }
    }

    /// <summary>
    /// Aplicar el cambio de vida
    /// </summary>
    /// <param name="change">Cambio a aplicar</param>
    protected void ChangeLife(EffectOutput effectChange) {

        if (effectChange.colorEffect)
        {
            lifeColor.ApplyChange(effectChange);
        }

        if (Mathf.Approximately(effectChange.LifeChangeRaw, 0.0f))
        {
            return;
        }

        life += effectChange.LifeChangeModified;

        if (life > maxLife)
        {
            life = maxLife;
        }

        bool death = life <= 0;

        if (death)
        {
            life = 0;
            continueApplying.Clear();
            lifeColor.StopAll();
        }
             
        onLifeChange.Invoke(this);

        if (effectChange.lifeChange >= 0)
        {
            onLifeChangeHeal.Invoke(this);
        } else {
            onLifeChangeDamage.Invoke(this);
        }

        if (death)
        {
            onLifeEnd.Invoke(this);
        }

    }

    /// <summary>
    /// Revivir al jugador
    /// </summary>
    public virtual void Revive() {
        life = maxLife;
        onLifeReStart.Invoke(this);
    }

    /// <summary>
    /// Muestra en Consola el Cambio de Vida
    /// </summary>
    /// <param name="lm">LifeManager a medir</param>
    public void DebugLifeChange(LifeManager lm) {
        Debug.Log("LifeChange " + lm.Life.ToString(), lm);
    }
}
