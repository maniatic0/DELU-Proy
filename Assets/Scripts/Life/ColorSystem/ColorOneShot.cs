using UnityEngine;

[CreateAssetMenu(fileName = "ColorOneShot", menuName = "Effects/Color/OneShot", order = 1)]
public class ColorOneShot : ColorEffect {
    /// <summary>
    /// Tiempo en el que se aplica el efecto del color
    /// </summary>
    [Tooltip("Tiempo en el que se aplica el efecto del color")]
    [SerializeField]
    protected float applyingTime = 0.3f;

    /// <summary>
    /// Si se continua aplicando el color
    /// </summary>
    /// <param name="deltaTime">DeltaTime</param>
    /// <param name="lm">Manejador de Vida Asociado</param>
    /// <param name="info">Info del del cambio</param>
    /// <returns>Si se Continua Aplicando Color</returns>
    public override bool ContinueAplying(float deltaTime, LifeManager lm, EffectOutput info) {
        return info.EffectInfo.GetElapsedTime() <= applyingTime;
    }
}