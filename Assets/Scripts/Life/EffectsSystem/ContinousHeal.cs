using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "ContinousHeal", menuName = "Effects/Heal/ContinousHeal", order = 2)]
public class ContinousHeal : EffectType {


    /// <summary>
    /// Curacion Basica
    /// </summary>
    [Tooltip("Curacion Basica")]
    [SerializeField]
    protected float basicHeal = 0;

    /// <summary>
    /// Tiempo entre la aplicacion de cada da
    /// </summary>
    [Tooltip("Daño Basico")]
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
    /// Curacion a aplicar
    /// </summary>
    /// <param name="mg">Manejador de Vida al se le esta aplicando la Curacion</param>
    /// <param name="info">Informacion del generador de la Curacion</param>
    /// <returns>Curacion a aplicar</returns>
    public override EffectOutput ApplyChange(LifeManager mg, EffectInfo info) {

        EffectOutput ans = new EffectOutput(info);

        if (info.AccumulatedTime >= reloadTime || !info.Started)
        {
            ans.lifeChange = basicHeal;
            ans.colorEffect = AppliedColorEffect;
            info.AccumulatedTime = 0f;
            info.Started = true; 
        }

        return ans;
    }
}