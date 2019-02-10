using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class LifeColorHandler : MonoBehaviour
{

    /// <summary>
    /// Efectos a aplicar en update
    /// </summary>
    /// <typeparam name="EffectOutput"></typeparam>
    /// <returns></returns>
    private List<EffectOutput> continueApplying = new List<EffectOutput>();

    /// <summary>
    /// Manejador de Vida Asociado
    /// </summary>
    private LifeManager lm;

    /// <summary>
    /// Renderer del sprite
    /// </summary>
    protected Renderer rend;

    /// <summary>
    /// Color Orginal del Sprite
    /// </summary>
    protected Color starterColor;

    /// <summary>
    /// Color a Colocar
    /// </summary>
    protected Color newColor;

    /// <summary>
    /// Cantidad de Colores aplicados
    /// </summary>
    protected int colorAmount;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        rend = GetComponent<Renderer>();
        starterColor = rend.material.color;
    }

    /// <summary>
    /// Configurar Manejador de Vida asociado
    /// </summary>
    /// <param name="life">Manejador de Vida</param>
    public void SetupLifeManager(LifeManager life) {
        lm = life;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    protected virtual void Update()
    {
        ApplyColors();
    }


    /// <summary>
    /// Aplicar Cambios de Colores
    /// </summary>
    protected virtual void ApplyColors() {
        newColor = starterColor;
        colorAmount = 1;
        for (int i = continueApplying.Count - 1; i > 0; i--)
        {
            if (!continueApplying[i].colorEffect.ContinueAplying(
                Time.deltaTime, 
                lm, 
                continueApplying[i])
                ) {
                continueApplying.RemoveAt(i);
            }
            else
            {
                newColor += continueApplying[i].colorEffect.ColorToApply(
                    Time.deltaTime, lm, continueApplying[i]
                );
                colorAmount++;
            }
        }
        rend.material.color = newColor / (float) colorAmount;
    }

    /// <summary>
    /// Aplicar cambio al color
    /// </summary>
    /// <param name="effect">Cambio a Aplicar</param>
    public virtual void ApplyChange(EffectOutput effectInfo) {
        continueApplying.Add(effectInfo);
    }
}
