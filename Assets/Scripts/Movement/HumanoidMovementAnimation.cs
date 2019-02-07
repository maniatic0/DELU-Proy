using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FliptEvent : UnityEvent<bool> {}

[RequireComponent(typeof(HumanoidMovement))]
[RequireComponent(typeof(SpriteFlip))]
[RequireComponent(typeof(Animator))]
public class HumanoidMovementAnimation : MonoBehaviour
{

    /// <summary>
    /// Componente para hacer flip de humanoide
    /// </summary>
	private SpriteFlip spriteFlip;

    /// <summary>
    /// Componente de manejo de movimiento
    /// </summary>
    private HumanoidMovement humanoidMovement;

    /// <summary>
    /// Si el humanoide esta viendo a la derecha
    /// Se usa para no hacer flip si esta viendo a la izquiera con cero velocidad
    /// </summary>
	private bool isFacingRight = true;

    /// <summary>
    /// Animator del Humanoide
    /// </summary>
    private Animator ani;

    /// <summary>
    /// Animator Integer Parameter para Velocidad en X
    /// </summary>
    [Tooltip("Animator Integer Parameter para Velocidad en X")]
    [SerializeField]
    private string animatorParamVelX = "VelX";

    /// <summary>
    /// Animator Parameter para Velocidad en Y
    /// </summary>
    [Tooltip("Animator Integer Parameter para Velocidad en Y")]
    [SerializeField]
    private string animatorParamVelY = "VelY";

    /// <summary>
    /// Animator Parameter para Velocidad en Z
    /// </summary>
    [Tooltip("Animator Integer Parameter para Velocidad en Z")]
    [SerializeField]
    private string animatorParamVelZ = "VelZ";


    /// <summary>
    /// Funciones llamadas cuando el sprite se voltea
    /// </summary>
    [Tooltip("Funciones llamadas cuando el sprite se voltea")]
    [SerializeField]
    public FliptEvent onFlipChange;

    void Awake()
    {
        spriteFlip = GetComponent <SpriteFlip> ();
        humanoidMovement = GetComponent<HumanoidMovement>();
        ani = GetComponent<Animator>();
    }

    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void LateUpdate()
    {
        Flip();
        UpdateAnimator();
    }

    /// <summary>
    /// Voltea el sprite del humanoide a la direccion del movimiento
    /// </summary>
	void Flip() {
        float velX = humanoidMovement.Velocity.x;
		if(velX < 0 && isFacingRight) {
            spriteFlip.FlipX = true;
            isFacingRight = false;
            onFlipChange.Invoke(isFacingRight);
        } else if(velX > 0 && !isFacingRight) {
            spriteFlip.FlipX = false;
            isFacingRight = true;
            onFlipChange.Invoke(isFacingRight);
        }
	}

    /// <summary>
    /// Actualizar los parametros del animator
    /// </summary>
    void UpdateAnimator() {
        ani.SetInteger(animatorParamVelX, Sign(humanoidMovement.Velocity.x));
        ani.SetInteger(animatorParamVelY, Sign(humanoidMovement.Velocity.y));
        ani.SetInteger(animatorParamVelZ, Sign(humanoidMovement.Velocity.z));
    }

    /// <summary>
    /// Devuelve el signo de un valor, si es 0 su signo es 0
    /// </summary>
    /// <param name="t">Valor al cual obtener signo</param>
    /// <returns>Signo</returns>
    static int Sign(float t) {
        if (t > 0)
        {
            return 1;
        } else if (t < 0) {
            return -1;
        }
        return 0;
    }
}
