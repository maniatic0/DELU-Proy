using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HumanoidMovement : MonoBehaviour
{
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
    /// Velocidad actual
    /// </summary>
    private Vector3 vel = Vector3.zero;

    /// <summary>
    /// Velocidad actual
    /// </summary>
    public Vector3 Velocity { get { return vel; } }

    /// <summary>
    /// Si el movimiento esta activo
    /// </summary>
    [Tooltip("Si el movimiento esta activo")]
    [SerializeField]
    private bool activeMovement = true;

    /// <summary>
    /// Si el movimiento esta activo
    /// </summary>
    /// <value></value>
    public bool ActiveMovement {
        get{return activeMovement;}
        set{
            if (!value && activeMovement)
            {
                Stop();
            }
            activeMovement = value;
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Poner la velocidad actual de humanoide
    /// Si la velocidad esta sobre el maximo, esta se recorta al maximo
    /// </summary>
    /// <param name="vel_x">Velocidad en X</param>
    /// <param name="vel_y">Velocidad en Y</param>
    /// <param name="vel_z">Velocidad en Z</param>
    protected void SetVelocity(float vel_x, float vel_y, float vel_z)
    {
        vel.x = vel_x;
        vel.y = vel_y;
        vel.z = vel_z;
    }

    /// <summary>
    /// Poner la velocidad actual de humanoide
    /// Si la velocidad esta sobre el maximo, esta se recorta al maximo
    /// </summary>
    /// <param name="new_vel">Nueva velocidad</param>
    protected void SetVelocity(Vector3 new_vel)
    {
        vel = new_vel;
    }

    void FixedUpdate()
    {
        if (ActiveMovement)
        {
            Move();
        }
    }

    /// <summary>
    /// Coloca la Velocidad al humanoide
    /// Si la velocidad esta sobre el maximo, esta se recorta al maximo
    /// </summary>
	void Move()
    {
        maxSpeedSqr = maxSpeed * maxSpeed;
        curSpeedSqr = vel.sqrMagnitude;
        if (maxSpeedSqr < curSpeedSqr)
        {
            vel = (vel / Mathf.Sqrt(curSpeedSqr)) * maxSpeed;
        }
        rb.velocity = vel;
    }

    /// <summary>
    /// Poner la Velocidad en cero
    /// </summary>
    public virtual void Stop() {
        rb.velocity = Vector3.zero;
    }
}