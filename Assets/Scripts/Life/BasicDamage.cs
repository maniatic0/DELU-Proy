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
    /// <returns>Daño a aplicar</returns>
    public override float ApplyChange(LifeManager mg) {
        return -basicDamage;
    }
}