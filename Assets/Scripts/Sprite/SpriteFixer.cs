using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteFixer : MonoBehaviour {

    /// <summary>
    /// Si recibe sombras el sprite
    /// </summary>
    [SerializeField]
    [Tooltip("Si recibe sombras el sprite")]
    private bool receiveShadow = true;

    /// <summary>
    /// Tipo de sombras que causa el sprite
    /// </summary>
    [SerializeField]
    [Tooltip("Tipo de sombras que causa el sprite")]
    private UnityEngine.Rendering.ShadowCastingMode shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.TwoSided;
    
    /// <summary>
    /// Basado en https://forum.unity.com/threads/why-cant-sprites-gameobjects-cast-shadows.215461/
    /// </summary>
    private void Awake() {
        SpriteRenderer re = GetComponent<SpriteRenderer>();
        re.receiveShadows = receiveShadow;
        re.shadowCastingMode = shadowCastingMode;
        Destroy(this);
    }
}