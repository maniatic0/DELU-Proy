using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HumanoidMovement))]
[RequireComponent(typeof(SpriteFlip))]
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


    void Awake()
    {
        spriteFlip = GetComponent <SpriteFlip> ();
        humanoidMovement = GetComponent<HumanoidMovement>();
    }

    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void LateUpdate()
    {
        Flip();
    }

    /// <summary>
    /// Voltea el sprite del humanoide a la direccion del movimiento
    /// </summary>
	void Flip() {
		if(humanoidMovement.Velocity.x < 0 && isFacingRight) {
            spriteFlip.FlipX = true;
            isFacingRight = false;
        } else if(humanoidMovement.Velocity.x > 0 && !isFacingRight) {
            spriteFlip.FlipX = false;
            isFacingRight = true;
        }
	}
}
