using UnityEngine;

/// <summary>
/// Un efecto y su origen
/// </summary>
public class EffectInfo
{
    /// <summary>
    /// Origen del Efecto
    /// </summary>
    public GameObject Origin {get; private set;}

    /// <summary>
    /// Efecto siendo aplicado
    /// </summary>
    public EffectType Effect {get; private set;}

    /// <summary>
    /// Cuando se inicio el efecto
    /// </summary>
    public float StartTime {get; private set;} = -1f;

    /// <summary>
    /// Acumulador de tiempo
    /// </summary>
    public float AccumulatedTime {get; set;} = 0f;

    /// <summary>
    /// Si ya fue iniciado el efecto
    /// </summary>
    public bool Started {get; set;} = false;

    private EffectInfo() {}

    /// <summary>
    /// Crea un EffectInfo con el Tipo de efecto y el origen
    /// </summary>
    /// <param name="info">Tipo de Efecto</param>
    /// <param name="ori">Origen</param>
    public EffectInfo(EffectType info, GameObject ori) {
        Effect = info;
        Origin = ori;
    }

    /// <summary>
    /// Crea un EffectInfo basado en otro EffectInfo
    /// </summary>
    /// <param name="other">Otro EffectInfo</param>
    public EffectInfo(EffectInfo other) {
        Effect = other.Effect;
        Origin = other.Origin;
        StartTime = other.StartTime;
    }

    /// <summary>
    /// Crea un EffectInfo basado en otro EffectInfo y tiempo de creacion
    /// </summary>
    /// <param name="other">Otro EffectInfo</param>
    /// <param name="start">Tiempo de creacion</param>
    public EffectInfo(EffectInfo other, float start) {
        Effect = other.Effect;
        Origin = other.Origin;
        StartTime = start;
    }

    /// <summary>
    /// Crea un nuevo EffectInfo basado en este EffectInfo e inicia startTime
    /// </summary>
    /// <returns>Nuevo EffectInfo</returns>
    public EffectInfo StartTimeCount() {
        return new EffectInfo(this, Time.time);
    }


    /// <summary>
    /// Obtener el tiempo pasado desde el inicio del efecto
    /// </summary>
    /// <returns>Tiempo pasado desde el inicio del efecto</returns>
    public float GetElapsedTime() {
        return Time.time - StartTime;
    }
}