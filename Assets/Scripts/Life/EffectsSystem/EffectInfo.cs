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

    /// <summary>
    /// Modificador a los efectos base
    /// </summary>
    /// <value></value>
    public float Modifier {get; private set;} = 1f;

    //private EffectInfo() {}

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
    /// Crea un EffectInfo con el Tipo de efecto, el origen y el modificador aplicado
    /// </summary>
    /// <param name="info">Tipo de Efecto</param>
    /// <param name="ori">Origen</param>
    /// <param name="modifier">Modificador del efecto</param>
    public EffectInfo(EffectType info, GameObject ori, float modifier) {
        Effect = info;
        Origin = ori;
        Modifier = modifier;
    }

    /// <summary>
    /// Crea un EffectInfo basado en otro EffectInfo
    /// </summary>
    /// <param name="other">Otro EffectInfo</param>
    public EffectInfo(EffectInfo other) {
        Effect = other.Effect;
        Origin = other.Origin;
        StartTime = other.StartTime;
        Modifier = other.Modifier;
    }

    /// <summary>
    /// Crea un EffectInfo basado en otro EffectInfo y tiempo de creacion
    /// </summary>
    /// <param name="other">Otro EffectInfo</param>
    /// <param name="start">Tiempo de creacion</param>
    public EffectInfo(EffectInfo other, float start) {
        Effect = other.Effect;
        Origin = other.Origin;
        Modifier = other.Modifier;
        StartTime = start;
    }

    /// <summary>
    /// Crea un EffectInfo basado en otro EffectInfo, modificador de cambio y  tiempo de creacion
    /// </summary>
    /// <param name="other">Otro EffectInfo</param>
    /// <param name="modifier">Modificador del efecto</param>
    /// <param name="start">Tiempo de creacion</param>
    public EffectInfo(EffectInfo other, float modifier, float start) {
        Effect = other.Effect;
        Origin = other.Origin;
        Modifier = modifier;
        StartTime = start;
    }

    /// <summary>
    /// Crea un nuevo EffectInfo basado en este EffectInfo e inicia startTime
    /// Ademas, se incluye cambio al modificador de daño
    /// </summary>
    /// <param name="modifier">Modificador del efecto</param>
    /// <returns>Nuevo EffectInfo</returns>
    public EffectInfo StartTimeCount(float modifier) {
        return new EffectInfo(this, modifier, Time.time);
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

    /// <summary>
    /// Calcula el cambio al manejador de vida
    /// </summary>
    /// <param name="lm">Manejador de Vida</param>
    /// <returns>Cambio a aplicar</returns>
    public EffectOutput ApplyChange(LifeManager lm) {
        EffectOutput res = Effect.ApplyChange(lm, this);
        res.modifier = Modifier;
        return res;
    }
}