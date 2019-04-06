using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAnimation : HumanoidMovementAnimation
{
    //revisar el if/if else, falta alguna cond
     protected override void Flip()
    {
        Debug.Log(AtRight(PlayerInput.MousePosition));
        float velX = humanoidMovement.Velocity.x;
        if ( (velX < 0 && isFacingRight) || !AtRight(PlayerInput.MousePosition))
        {
            spriteFlip.FlipX = true;
            isFacingRight = false;
            onFlipChange.Invoke(isFacingRight);
        }
        else if ( (velX > 0 && !isFacingRight) || AtRight(PlayerInput.MousePosition))
        {
            spriteFlip.FlipX = false;
            isFacingRight = true;
            onFlipChange.Invoke(isFacingRight);
        }
    }

    bool AtRight(Vector3 pos)
    {
        Vector2 worldPos = (Camera.main).ScreenToWorldPoint(pos + Vector3.forward*10);
        Debug.Log(PlayerInput.MousePosition);
        Debug.Log("Player x: " + transform.position.x + " Mouse X: " + worldPos.x);
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
