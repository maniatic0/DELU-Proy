using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class LifetEvent : UnityEvent<LifeManager> {}

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
    public LifetEvent onLifeChange;

    /// <summary>
    /// Funciones para llamar cuando la cantidad de vida cambia de forma positiva
    /// </summary>
    [Tooltip("Funciones para llamar cuando la cantidad de vida cambia de forma positiva")]
    public LifetEvent onLifeChangeHeal;

    /// <summary>
    /// Funciones para llamar cuando la cantidad de vida cambia de forma negativa
    /// </summary>
    [Tooltip("Funciones para llamar cuando la cantidad de vida cambia de forma negativa")]
    public LifetEvent onLifeChangeDamage;

    /// <summary>
    /// Funciones para llamar cuando muere el humanoide
    /// </summary>
    [Tooltip("Funciones para llamar cuando muere el humanoide")]
    public LifetEvent onLifeEnd;

    /// <summary>
    /// Funciones para llamar cuando revive el humanoide
    /// </summary>
    [Tooltip("Funciones para llamar cuando revive el humanoide")]
    public LifetEvent onLifeReStart;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected virtual void Awake()
    {
        life = maxLife;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    protected virtual void Update()
    {
        for (int i = continueApplying.Count; i > 0; i--)
        {
            if (!continueApplying[i].effect.ContinueApplyingChange(
                Time.deltaTime, 
                this, 
                continueApplying[i])
                ) {
                continueApplying.RemoveAt(i);
            }
            else
            {
                ChangeLife(continueApplying[i].effect.ApplyChange(this, continueApplying[i]));
            }
        }
    }

    /// <summary>
    /// Aplicar cambio a la vida
    /// </summary>
    /// <param name="effect">Cambio a Aplicar</param>
    public virtual void ApplyChange(EffectInfo effectInfo) {
        if (effectInfo.effect.ApplyMoreThanOnce)
        {
            continueApplying.Add(effectInfo);
        }
        else {
            ChangeLife(effectInfo.effect.ApplyChange(this, effectInfo));
        }
    }

    /// <summary>
    /// Aplicar el cambio de vida
    /// </summary>
    /// <param name="change">Cambio a aplicar</param>
    protected void ChangeLife(EffectOutput effectChange) {

        if (Mathf.Approximately(effectChange.lifeChange, 0.0f))
        {
            return;
        }

        life += effectChange.lifeChange;

        if (life > maxLife)
        {
            life = maxLife;
        }

        bool death = life <= 0;

        if (death)
        {
            life = 0;
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
