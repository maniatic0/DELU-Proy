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
    /// <param name="info">Info del generador de la curacion</param>
    /// <returns>Curacion a aplicar</returns>
    public override EffectOutput ApplyChange(LifeManager mg, EffectInfo info) {
        EffectOutput ans;
        ans.lifeChange = basicHeal;
        return ans;
    }
}