using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "BasicPurification", menuName = "Effects/Purification/BasicPurification", order = 1)]
public class BasicPurification : EffectType {

    
    /// <summary>
    /// Purificacion Basica
    /// </summary>
    [Tooltip("Purificacion Basica")]
    [SerializeField]
    protected float basicPurification = 0;

    
    /// <summary>
    /// Purificacion a aplicar
    /// </summary>
    /// <param name="mg">Manejador de Vida al se le esta aplicando la Purificacion</param>
    /// <param name="info">Informacion del generador de la Purificacion</param>
    /// <returns>Purificacion a aplicar</returns>
    public override EffectOutput ApplyChange(LifeManager mg, EffectInfo info) {

        EffectOutput ans = new EffectOutput(info);
        ans.corruptionChange = -basicPurification;
        ans.colorEffect = AppliedColorEffect;
        return ans;
    }
}