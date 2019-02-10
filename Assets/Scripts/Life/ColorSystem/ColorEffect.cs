using UnityEngine;
using System.Collections;

/// <summary>
/// Clase base para aplicar Efectos de Colores
/// </summary>
public class ColorEffect : ScriptableObject { 

    /// <summary>
    /// Color a aplicar
    /// </summary>
    [Tooltip("Color a aplicar")]
    [SerializeField]
    protected Color colorToApply = Color.white;

    /// <summary>
    /// Si se continua aplicando el color
    /// </summary>
    /// <param name="deltaTime">DeltaTime</param>
    /// <param name="lm">Manejador de Vida Asociado</param>
    /// <param name="info">Info del cambio</param>
    /// <returns>Si se Continua Aplicando Color</returns>
    public virtual bool ContinueAplying(float deltaTime, LifeManager lm, EffectOutput info) {
        return false;
    }

    /// <summary>
    /// Color a aplicar
    /// </summary>
    /// <param name="deltaTime">DeltaTime</param>
    /// <param name="lm">Manejador de Vida Asociado</param>
    /// <param name="info">Info del del cambio</param>
    /// <returns>Color a aplicar</returns>
    public virtual Color ColorToApply(float deltaTime, LifeManager lm, EffectOutput info) {
        return colorToApply;
    }
}