using UnityEngine;

/// <summary>
/// Output de aplicar un efecto
/// </summary>
public class EffectOutput
{
    /// <summary>
    /// Effect Info que creo el output
    /// </summary>
    public EffectInfo EffectInfo {get; private set;}

    /// <summary>
    /// Cambio en la vida
    /// </summary>
    public float lifeChange = 0;

    /// <summary>
    /// Modificador del cambio en la vida
    /// </summary>
    public float modifier = 1;

    /// <summary>
    /// Obtener el cambio de vida sin modificador
    /// </summary>
    /// <value></value>
    public float LifeChangeRaw { get{return lifeChange;}}

    /// <summary>
    /// Obtener el cambio de vida con modificador
    /// </summary>
    /// <value></value>
    public float LifeChangeModified { get{return LifeChangeRaw * modifier;}}

    /// <summary>
    /// Efecto de color siendo aplicado
    /// </summary>
    public ColorEffect colorEffect = null;

    //private EffectOutput() {}

    /// <summary>
    /// Crea un EffectOutput con la info de quien lo creo
    /// </summary>
    /// <param name="info">Effect Info que creo el output</param>
    public EffectOutput(EffectInfo info) {
        EffectInfo = info;
    }
}