using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class SpriteFlip : MonoBehaviour
{
    /// <summary>
    /// Ecala cuando esta normal el sprite
    /// </summary>
    private Vector2 normalScale = new Vector2(1.0f, 1.0f);

    /// <summary>
    /// Ecala cuando esta invertido el sprite
    /// </summary>
    private Vector2 flippedScale = new Vector2(-1.0f, 1.0f);

    /// <summary>
    /// Offset de sprite en estado normal
    /// </summary>
    private Vector2 normalOffset = new Vector2(0, 0);

    /// <summary>
    /// Offset de sprite en estado invertido
    /// </summary>
    private Vector2 flippedOffset = new Vector2(1.0f, 0);

    /// <summary>
    /// Renderer del sprite
    /// </summary>
    Renderer rend;

    /// <summary>
    /// Si se esta invirtiendo x
    /// </summary>
    private bool _flipX = false;

    /// <summary>
    /// Si se esta invirtiendo x
    /// </summary>
    public bool FlipX { get { return _flipX; } set { ChangeFlipState(value); } }

    /// <summary>
    /// Cambiar el estado del flip a state
    /// /// </summary>
    /// <param name="state">Nuevo estado del flip</param>
    public void ChangeFlipState(bool state)
    {
        _flipX = state;
        if (_flipX)
        {
            rend.material.mainTextureScale = flippedScale;
            rend.material.mainTextureOffset = flippedOffset;
        }
        else
        {
            rend.material.mainTextureScale = normalScale;
            rend.material.mainTextureOffset = normalOffset;
        }
    }

    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }
}
