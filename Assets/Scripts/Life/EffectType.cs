using UnityEngine;
using System.Collections;

public class EffectType : ScriptableObject {

    /// <summary>
    /// Si el cambio se aplica más de una vez
    /// </summary>
    [Tooltip("Si el cambio se aplica más de una vez")]
    [SerializeField]
    protected bool applyMoreThanOnce = false;

    public bool ApplyMoreThanOnce {get{return applyMoreThanOnce;}}

    /// <summary>
    /// Si se continua aplicando el cambio 
    /// </summary>
    /// <param name="delta">Delta Time</param>
    /// <param name="mg">Manejador de Vida al que se le continua aplicando el cambio</param>
    /// <param name="info">Info del generador del cambio</param>
    /// <returns>Si se continua aplicando el cambio</returns>
    public virtual bool ContinueApplyingChange(float delta, LifeManager mg, EffectInfo info) {
        return false;
    }

    
    /// <summary>
    /// Cambio a aplicar
    /// </summary>
    /// <param name="mg">Manejador de Vida al se le esta aplicando el cambio</param>
    /// <param name="info">Info del generador del cambio</param>
    /// <returns>Cambio a aplicar</returns>
    public virtual EffectOutput ApplyChange(LifeManager mg, EffectInfo info) {

        EffectOutput ans;

        ans.lifeChange = 0;

        return ans;
    }

}