using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreTags;

[RequireComponent(typeof(Collider))]
public class ContactTrap : MonoBehaviour
{
    /// <summary>
    /// Tiempo de inmunidad a golpes de esta trampa
    /// </summary>
    [Tooltip("Tiempo de inmunidad a golpes de esta trampa")]
    [SerializeField]
    private float reloadTime = 0.2f;

    /// <summary>
    /// Daño a aplicar
    /// </summary>
    [Tooltip("Daño a aplicar")]
    [SerializeField]
    private EffectType damageType;

    /// <summary>
    /// Info del daño a aplicar
    /// </summary>
    private EffectInfo info;

    /// <summary>
    /// Objetos que se consideran inmunes al daño de esta trampa
    /// </summary>
    /// <typeparam name="Collision"></typeparam>
    /// <returns></returns>
    private HashSet<Collider> reloading = new HashSet<Collider>();

    /// <summary>
    /// Patron de los tags de gameObjecto que seran afectados por la trampa
    /// </summary>
    [Tooltip("Patron de los tags de gameObjecto que seran afectados por la trampa")]
    [SerializeField]
    private string targetsPattern = "Damageable.*";

    /// <summary>
    /// Tags relacionados al patron
    /// </summary>
    private string[] targetPatternInternal;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        info = new EffectInfo(damageType , this.gameObject);
        targetPatternInternal = TagUtilities.PatternToStrings(targetsPattern);
    }


    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        HandleContact(other);
    }


    /// <summary>
    /// OnCollisionStay is called once per frame for every collider/rigidbody
    /// that is touching rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionStay(Collision other)
    {
        HandleContact(other);
    }


    /// <summary>
    /// Maneja el contacto con algun objeto
    /// </summary>
    /// <param name="other">Objecto con el que se choco</param>
    void HandleContact(Collision other) {
        if (!reloading.Contains(other.collider))
        {
            LifeManager lm = other.gameObject.GetComponent<LifeManager>();
            if (lm && other.gameObject.AnyTags(targetPatternInternal))
            {
                lm.ApplyChange(info.StartTimeCount());
                StartCoroutine(Reload(other.collider));
            }
        }
    }

    /// <summary>
    /// Aplica el tiempo de inmunidad a golpes de esta trampa
    /// </summary>
    /// <param name="other">Collider al que se le acaba de aplicar daño</param>
    /// <returns></returns>
    IEnumerator Reload(Collider other) {
        reloading.Add(other);
        yield return new WaitForSeconds(reloadTime);
        reloading.Remove(other);
    }
}
