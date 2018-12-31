using UnityEngine;


public class CameraProy : MonoBehaviour
{
    /// <summary>
    /// Camara Principal
    /// </summary>
    [SerializeField]
    [Tooltip("Camara Principal")]
    private Camera cam;

    /// <summary>
    /// Angulo de la camara contra eje X
    /// </summary>
    [SerializeField]
    [Tooltip("Angulo de la camara contra eje X")]
    private float angle = 60f;

    /// <summary>
    /// Angulo de la camara contra eje X
    /// </summary>
    public float Angle { get { return angle; } set { angle = value; CalculateProjection(); } }

    /// <summary>
    /// Nivel de Zoom de la Camara
    /// </summary>
    [SerializeField]
    [Tooltip("Nivel de Zoom de la Camara")]
    private float zoomLevel = 10f;

    /// <summary>
    /// Nivel de Zoom de la Camara
    /// </summary>
    public float ZoomLevel { get { return zoomLevel; } set { zoomLevel = value; CalculateZoom(); } }

    /// <summary>
    /// Seno del Angulo inverso, usado para cambio de proyeccion 
    /// </summary>
    private float inv_angle;


    /// <summary>
    /// Proyeccion Anterior
    /// </summary>
    private Matrix4x4 prevProy;

    /// <summary>
    /// Vector director para calcular zoom
    /// </summary>
    private Vector3 zoomVector;

    private void Awake()
    {
        CalculateProjection();
    }

#if UNITY_EDITOR
    private void OnPreCull()
    {
        CalculateProjection();
        CalculateZoom();
    }
#endif

    /// <summary>
    /// Cambiar la proyeccion de la camara para arreglar sprites
    /// </summary>
    public void CalculateProjection()
    {
        inv_angle = 1.0f / Mathf.Sin(angle); // Invertir efecto de squash causado por el angulo

        cam.ResetProjectionMatrix();

        prevProy = cam.projectionMatrix;
        prevProy[1, 1] *= inv_angle;
        cam.projectionMatrix = prevProy;

        // Vector Zoom viene de Vector contra el angulo del eje x
        zoomVector = Quaternion.AngleAxis(angle, Vector3.right) * Vector3.up;
        zoomVector.z *= -1.0f; // Invertir Z para camara
        CalculateZoom();

    }

    /// <summary>
    /// Calcular zoom de camara usando vector de zoom
    /// </summary>
    public void CalculateZoom()
    {
        cam.transform.localPosition = zoomLevel * zoomVector;
    }
}
