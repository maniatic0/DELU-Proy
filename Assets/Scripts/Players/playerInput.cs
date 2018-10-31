using UnityEngine;

/// <summary>
/// Manejador de Inputs del jugador
/// </summary>
public class playerInput : MonoBehaviour
{

    /// <summary>
    /// Manejador actualmente activo
    /// Solo existe uno a la vez
    /// </summary>
    /// <value></value>
    public static playerInput Manager { get; private set; }

    /// <summary>
    /// Vector de Input de los Axis Horizontal y Vertical
    /// </summary>
    /// <value></value>
    public static Vector2 Axis
    {
        get { return Manager._axis; }
    }

    /// <summary>
    /// Vector de Raw Input de los Axis Horizontal y Vertical
    /// </summary>
    /// <value></value>
    public static Vector2 AxisRaw
    {
        get { return Manager._axisRaw; }
    }

    /// <summary>
    /// Vector de Input de los Axis Horizontal y Vertical
    /// </summary>
    private Vector2 _axis = Vector2.zero;

    /// <summary>
    /// Vector de Raw Input de los Axis Horizontal y Vertical
    /// </summary>
    private Vector2 _axisRaw = Vector2.zero;

    /// <summary>
    /// Nombre del Input Horizontal
    /// </summary>
    [SerializeField]
    [Tooltip("Nombre del Input Horizontal")]
    private string horizontalAxis = "Horizontal";

    /// <summary>
    /// Nombre del Input Vertical
    /// </summary>
    [SerializeField]
    [Tooltip("Nombre del Input Vertical")]
    private string verticalAxis = "Vertical";

    /// <summary>
    /// Boton de Salto Presionado
    /// </summary>
    /// <value></value>
    public static bool JumpDown
    {
        get { return Manager.jumpDown; }
    }

    /// <summary>
    /// Boton de Salto Presionandose
    /// </summary>
    /// <value></value>
    public static bool Jump
    {
        get { return Manager.jump; }
    }

    /// <summary>
    /// Boton de Salto Liberado
    /// </summary>
    /// <value></value>
    public static bool JumpUp
    {
        get { return Manager.jumpUp; }
    }

    /// <summary>
    /// Boton de Salto Presionado
    /// </summary>
    private bool jumpDown = false;

    /// <summary>
    /// Boton de Salto Presionandose
    /// </summary>
    private bool jump = false;

    /// <summary>
    /// Boton de Salto Liberado
    /// </summary>
    private bool jumpUp = false;

    /// <summary>
    /// Nombre del Input de Salto
    /// </summary>
    [SerializeField]
    [Tooltip("Nombre del Input Vertical")]
    private string jumpButton = "Jump";


    private void Awake()
    {
        if (Manager != null && Manager != this)
        {
            Debug.LogError("PlayerInput Duplicado", this.gameObject);
            Destroy(this);
        }
        Manager = this;
    }

    private void Update()
    {

        _axisRaw.x = Input.GetAxisRaw(horizontalAxis);
        _axis.x = Input.GetAxis(horizontalAxis);

        _axisRaw.y = Input.GetAxisRaw(verticalAxis);
        _axis.y = Input.GetAxis(verticalAxis);

        jumpDown = Input.GetButtonDown(jumpButton);
        jump = Input.GetButton(jumpButton);
        jumpUp = Input.GetButtonUp(jumpButton);
    }

}