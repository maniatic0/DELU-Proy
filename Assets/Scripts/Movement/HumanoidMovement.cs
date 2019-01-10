using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SpriteFlip))]
public class HumanoidMovement : MonoBehaviour {
    /// <summary>
	/// Velocidad maxima a la que ira el humanoide
	/// </summary>
    [Tooltip("Velocidad maxima a la que ira el humanoide")]   
	[SerializeField] 
    protected float maxSpeed = 7f;

    /// <summary>
    /// Maxima velocidad al cuadrado
    /// </summary>
    private float maxSpeedSqr;

    /// <summary>
    /// Velocidad actual al cuadrado
    /// </summary>
    private float curSpeedSqr;

    /// <summary>
    /// Rigidbody de humanoide
    /// </summary>
	private Rigidbody rb;

    /// <summary>
    /// Componente para hacer flip de humanoide
    /// </summary>
	private SpriteFlip spriteFlip;

    /// <summary>
    /// Si el humanoide esta viendo a la derecha
    /// Se usa para no hacer flip si esta viendo a la izquiera con cero velocidad
    /// </summary>
	private bool isFacingRight = true;

    /// <summary>
    /// Velocidad actual
    /// </summary>
    private Vector3 vel = Vector3.zero;

	void Awake() {
		rb = GetComponent <Rigidbody> ();
		spriteFlip = GetComponent <SpriteFlip> ();
	}

    /// <summary>
    /// Poner la velocidad actual de humanoide
    /// Si la velocidad esta sobre el maximo, esta se recorta al maximo
    /// </summary>
    /// <param name="vel_x">Velocidad en X</param>
    /// <param name="vel_y">Velocidad en Y</param>
    /// <param name="vel_z">Velocidad en Z</param>
    protected void SetVelocity(float vel_x, float vel_y, float vel_z) {
        vel.x = vel_x;
        vel.y = vel_y;
        vel.z = vel_z;
    }

    /// <summary>
    /// Poner la velocidad actual de humanoide
    /// Si la velocidad esta sobre el maximo, esta se recorta al maximo
    /// </summary>
    /// <param name="new_vel">Nueva velocidad</param>
    protected void SetVelocity(Vector3 new_vel) {
        vel = new_vel;
    }

	void FixedUpdate() {
		Move();
		Flip();
	}

    /// <summary>
    /// Coloca la Velocidad al humanoide
    /// Si la velocidad esta sobre el maximo, esta se recorta al maximo
    /// </summary>
	void Move() {
        maxSpeedSqr = maxSpeed * maxSpeed;
        curSpeedSqr = vel.sqrMagnitude;
        if (maxSpeedSqr < curSpeedSqr)
        {
            vel = (vel / Mathf.Sqrt(curSpeedSqr)) * maxSpeed;
        }
		rb.velocity = vel;
	}

    /// <summary>
    /// Voltea el sprite del humanoide a la direccion del movimiento
    /// </summary>
	void Flip() {
		if(rb.velocity.x < 0 && isFacingRight) {
            spriteFlip.FlipX = true;
            isFacingRight = false;
        } else if(rb.velocity.x > 0 && !isFacingRight) {
            spriteFlip.FlipX = false;
            isFacingRight = true;
        }
	}

}