using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "BasicDamage", menuName = "Effects/Damage/BasicDamage", order = 1)]
public class BasicDamage : EffectType {

    
    /// <summary>
    /// Daño Basico
    /// </summary>
    [Tooltip("Daño Basico")]
    [SerializeField]
    protected float basicDamage = 0;

    
    /// <summary>
    /// Daño a aplicar
    /// </summary>
    /// <param name="mg">Manejador de Vida al se le esta aplicando el Daño</param>
    /// <param name="info">Informacion del generador del daño</param>
    /// <returns>Daño a aplicar</returns>
    public override EffectOutput ApplyChange(LifeManager mg, EffectInfo info) {

        EffectOutput ans = new EffectOutput(info);
        ans.lifeChange = -basicDamage;
        ans.colorEffect = AppliedColorEffect;
        return ans;
    }
}