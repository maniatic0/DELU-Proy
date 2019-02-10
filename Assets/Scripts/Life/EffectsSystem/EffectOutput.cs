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
    /// Efecto de color siendo aplicado
    /// </summary>
    public ColorEffect colorEffect = null;

    private EffectOutput() {}

    /// <summary>
    /// Crea un EffectOutput con la info de quien lo creo
    /// </summary>
    /// <param name="info">Effect Info que creo el output</param>
    public EffectOutput(EffectInfo info) {
        EffectInfo = info;
    }
}