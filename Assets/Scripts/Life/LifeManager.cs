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
    /// Corrupcion Actual
    /// </summary>
    private float corruption;

    /// <summary>
    /// Corrupcion Actual
    /// </summary>
    public float Corruption { 
        get{ 
            return corruption; 
            } 
        }

    /// <summary>
    /// Maxima Corrupcion
    /// </summary>
    [Tooltip("Maxima Corrupcion")]
    [SerializeField]
    private float maxCorruption = 10.0f;

    /// <summary>
    /// Maxima Corrupcion
    /// </summary>
    public float MaxCorruption { 
        get{ 
            return maxCorruption; 
            } 
        }

    /// <summary>
    /// Si el humanoide es afectado por cambios de corrupcion
    /// </summary>
    [Tooltip("Si el humanoide es afectado por cambios de corrupcion")]
    [SerializeField]
    private bool useCorruption = true;
        

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
    /// Funciones para llamar cuando la cantidad de corrupcion cambia
    /// </summary>
    [Tooltip("Funciones para llamar cuando la cantidad de corrupcion cambia")]
    public LifeEvent onCorruptionChange;

    /// <summary>
    /// Funciones para llamar cuando la cantidad de corrupcion cambia por purificacion
    /// </summary>
    [Tooltip("Funciones para llamar cuando la cantidad de corrupcion cambia por purificacion")]
    public LifeEvent onCorruptionChangePurify;

    /// <summary>
    /// Funciones para llamar cuando la cantidad de corrupcion cambia por mas corrupcion
    /// </summary>
    [Tooltip("Funciones para llamar cuando la cantidad de corrupcion cambia por mas corrupcion")]
    public LifeEvent onCorruptionChangeCorrupt;

    /// <summary>
    /// Funciones para llamar cuando el humanoide es purificado
    /// </summary>
    [Tooltip("Funciones para llamar cuando el humanoide es purificado")]
    public LifeEvent onPurification;

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
        corruption = maxCorruption;
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
                Change(continueApplying[i].ApplyChange(this));
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
            Change(effectInfo.ApplyChange(this));
        }
    }

    /// <summary>
    /// Aplicar el cambio
    /// </summary>
    /// <param name="effectChange">Cambio a aplicar</param>
    protected void Change(EffectOutput effectChange) {

        if (effectChange.colorEffect)
        {
            lifeColor.ApplyChange(effectChange);
        }

        life += effectChange.LifeChangeModified;

        if (life > maxLife)
        {
            life = maxLife;
        }

        if (useCorruption)
        {
            corruption += effectChange.CorruptionChangeModified;

            if (corruption > maxCorruption)
            {
                corruption = maxCorruption;
            }
        }

        bool death = life <= 0;

        if (death)
        {
            life = 0;
            continueApplying.Clear();
            lifeColor.StopAll();
        }

        bool purified = corruption <= 0;

        if (useCorruption && purified)
        {
            corruption = 0;
            continueApplying.Clear();
            lifeColor.StopAll();
        }

        if (effectChange.LifeChangeRaw > 0)
        {
            onLifeChange.Invoke(this);
            onLifeChangeHeal.Invoke(this);
        } else if (effectChange.LifeChangeRaw < 0) {
            onLifeChange.Invoke(this);
            onLifeChangeDamage.Invoke(this);
        }

        if (useCorruption)
        {
            if (effectChange.CorruptionChangeRaw > 0)
            {
                onCorruptionChange.Invoke(this);
                onCorruptionChangePurify.Invoke(this);
            } else if (effectChange.CorruptionChangeRaw < 0) {
                onCorruptionChange.Invoke(this);
                onCorruptionChangeCorrupt.Invoke(this);
            }
        }

        

        if (death)
        {
            onLifeEnd.Invoke(this);
            return;
        }

        if (useCorruption && purified)
        {
            onPurification.Invoke(this);
            return;
        }

    }

    /// <summary>
    /// Revivir al humanoide
    /// </summary>
    public virtual void Revive() {
        life = maxLife;
        corruption = maxCorruption;
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
