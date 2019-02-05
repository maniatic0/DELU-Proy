using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "BasicHeal", menuName = "Effects/Heal/BasicHeal", order = 1)]
public class BasicHeal : EffectType {

    
    /// <summary>
    /// Curacion Basica
    /// </summary>
    [Tooltip("Curacion Basica")]
    [SerializeField]
    protected float basicHeal = 0;

    /// <summary>
    /// Curacion a aplicar
    /// </summary>
    /// <param name="mg">Manejador de Vida al se le esta aplicando la curacion</param>
    /// <returns>Curacion a aplicar</returns>
    public override float ApplyChange(LifeManager mg) {
        return -basicHeal;
    }
}