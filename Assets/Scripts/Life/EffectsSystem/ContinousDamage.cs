using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "ContinousDamage", menuName = "Effects/Damage/ContinousDamage", order = 2)]
public class ContinousDamage : EffectType {


    /// <summary>
    /// Daño Basico
    /// </summary>
    [Tooltip("Daño Basico")]
    [SerializeField]
    protected float basicDamage = 0;

    /// <summary>
    /// Tiempo entre la aplicacion de cada tick del daño
    /// </summary>
    [Tooltip("Tiempo entre la aplicacion de cada tick del daño")]
    [SerializeField]
    protected float reloadTime = 0;

    /// <summary>
    /// Si se continua aplicando el cambio 
    /// </summary>
    /// <param name="delta">Delta Time</param>
    /// <param name="mg">Manejador de Vida al que se le continua aplicando el cambio</param>
    /// <param name="info">Info del generador del cambio</param>
    /// <returns>Si se continua aplicando el cambio</returns>
    public override bool ContinueApplyingChange(float delta, LifeManager mg, EffectInfo info) {
        info.AccumulatedTime += delta;
        return  info.GetElapsedTime() <= applyingTime;
    }
    
    /// <summary>
    /// Daño a aplicar
    /// </summary>
    /// <param name="mg">Manejador de Vida al se le esta aplicando el Daño</param>
    /// <param name="info">Informacion del generador del daño</param>
    /// <returns>Daño a aplicar</returns>
    public override EffectOutput ApplyChange(LifeManager mg, EffectInfo info) {

        EffectOutput ans = new EffectOutput(info);

        if (info.AccumulatedTime >= reloadTime || !info.Started)
        {
            ans.lifeChange = -basicDamage;
            ans.colorEffect = AppliedColorEffect;
            info.AccumulatedTime = 0f; 
            info.Started = true;
        }

        return ans;
    }
}