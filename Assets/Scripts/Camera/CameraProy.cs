using UnityEngine;

[ExecuteInEditMode]
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
    public float Angle { get { return angle; } set { angle = value; CalculateAngle(); } }

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
    private float invSinAngle;

    /// <summary>
    /// Coseno del Angulo inverso, usado para cambio de proyeccion 
    /// </summary>
    private float invCosAngle;


    /// <summary>
    /// Proyeccion Anterior
    /// </summary>
    private Matrix4x4 prevProy;

    /// <summary>
    /// Vector director para calcular zoom
    /// </summary>
    private Vector3 zoomVector;

    private void Awake() {
        if (cam == null)
        {
            cam = Camera.main;
        }
    }

    private void OnPreCull()
    {
        #if UNITY_EDITOR
        CalculateAngle();
        #endif // UNITY_EDITOR
        CalculateProjection();
    }

    /// <summary>
    /// Cambiar la proyeccion de la camara para arreglar sprites
    /// Basado en https://www.youtube.com/watch?v=zPQOHX9hiL0&feature=youtu.be&t=14m54s
    /// </summary>
    public void CalculateProjection()
    {
        cam.ResetProjectionMatrix();

        prevProy = cam.projectionMatrix;
        prevProy[1, 1] *= invCosAngle;
        prevProy[2, 2] *= invSinAngle;
        prevProy[2, 3] *= invSinAngle;
        prevProy[3, 2] *= invSinAngle;
        cam.projectionMatrix = prevProy;

    }

    /// <summary>
    /// Calcular zoom de camara usando vector de zoom
    /// </summary>
    public void CalculateZoom()
    {
        cam.transform.localPosition = zoomLevel * zoomVector;
    }

    /// <summary>
    /// Calular los efectos del angulo sobre la camara
    /// Basado en https://www.youtube.com/watch?v=zPQOHX9hiL0&feature=youtu.be&t=14m54s
    /// </summary>
    public void CalculateAngle() {
        invSinAngle = 1.0f / Mathf.Sin(angle * Mathf.Deg2Rad); // Invertir efecto de squash causado por el angulo
        invCosAngle = 1.0f / Mathf.Cos(angle * Mathf.Deg2Rad); // Invertir efecto de squash causado por el angulo
        
        // Vector Zoom viene de Vector contra el angulo del eje x
        cam.transform.rotation = Quaternion.AngleAxis(angle, Vector3.right);
        zoomVector = cam.transform.rotation * Vector3.forward;
        zoomVector.y *= -1.0f; // Invertir Y para camara
        zoomVector.z *= -1.0f; // Invertir Z para camara
        CalculateZoom();
    }
}
