using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(LifeManager))]
public class HumanoidDeathManager : MonoBehaviour {

    /// <summary>
    /// Manejador de Movimiento
    /// </summary>
    [Tooltip("Manejador de Movimiento")]
    [SerializeField]
    private HumanoidMovement movement;

    /// <summary>
    /// Manejador de Vida
    /// </summary>
    private LifeManager life;

    /// <summary>
    /// Manejador de Combate Melee
    /// </summary>
    [Tooltip("Manejador de Combate Melee")]
    [SerializeField]
    private HumanoidMeleeCombatHandler meleeManager;

    /// <summary>
    /// Animador para llamar al trigger de muerte
    /// </summary>
    private Animator ani;
    
    /// <summary>
    /// Nombre del Trigger de muerte en el Animator
    /// </summary>
    [Tooltip("Nombre del Trigger de muerte en el Animator")]
    [SerializeField]
    private string deathAnimatorTrigger = "Death";


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        life = GetComponent<LifeManager>();
        life.onLifeEnd.AddListener(OnDeath);

        ani = GetComponent<Animator>();

        if (!movement)
        {
            movement = GetComponent<HumanoidMovement>();
        }
        if (!meleeManager)
        {
            meleeManager = GetComponent<HumanoidMeleeCombatHandler>();
        }
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        if (life) {
            life.onLifeEnd.RemoveListener(OnDeath);
        }
    }

    /// <summary>
    /// Handle Death Event
    /// </summary>
    /// <param name="lm"></param>
    public virtual void OnDeath(LifeManager lm) {
        if (lm != life)
        {
            Debug.LogWarning("Death Manager got a different Life Manager", this);
        }

        if (movement)
        {
            movement.ActiveMovement = false;
        }

        if (meleeManager)
        {
            meleeManager.ActiveMeleeCombat = false;
        }

        ani.SetTrigger(deathAnimatorTrigger);
    }
}