﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerRangedHandler))]
public class PlayerMovementAnimation : HumanoidMovementAnimation
{
    /// <summary>
    /// Script de combate de rango del jugador
    /// </summary>
    private PlayerRangedHandler rangedHandler;

    void Awake()
    {
        spriteFlip = GetComponent<SpriteFlip>();
        humanoidMovement = GetComponent<HumanoidMovement>();
        ani = GetComponent<Animator>();
        rangedHandler = GetComponent<PlayerRangedHandler>();
    }

    /// <summary>
    /// Voltea el sprite del jugador respecto al movimiento (en modo melee)
    /// Voltea el sprite del jugador respecto al mouse (en modo rango)
    /// </summary>
    protected override void Flip()
    {
        float velX = humanoidMovement.Velocity.x;
        if (!rangedHandler.ActiveRanged)
        {
            if (velX < 0 && isFacingRight)
            {
                spriteFlip.FlipX = true;
                isFacingRight = false;
                onFlipChange.Invoke(isFacingRight);
            }
            else if (velX > 0 && !isFacingRight)
            {
                spriteFlip.FlipX = false;
                isFacingRight = true;
                onFlipChange.Invoke(isFacingRight);
            }
        }
        else
        {
            if (!AtRight(PlayerInput.MousePosition))
            {
                spriteFlip.FlipX = true;
                isFacingRight = false;
                onFlipChange.Invoke(isFacingRight);
            }
            else
            {
                spriteFlip.FlipX = false;
                isFacingRight = true;
                onFlipChange.Invoke(isFacingRight);
            }
        }
    }

    /// <summary>
    /// Devuelve un bool que indica si el mouse esta a la derecha del jugador
    /// </summary>
    /// <param name="mPos">Posicion del mouse</param>
    /// <returns>Si el mouse se encuentra a la derecha</returns>
    bool AtRight(Vector3 mPos)
    {
        mPos.z = -(Camera.main).transform.position.z;
        Vector2 worldPos = (Camera.main).ScreenToWorldPoint(mPos);
        if (transform.position.x < worldPos.x)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
