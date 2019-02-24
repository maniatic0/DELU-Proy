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
    /// Nombre del Trigger de revivir en el Animator
    /// </summary>
    [Tooltip("Nombre del Trigger de revivir en el Animator")]
    [SerializeField]
    private string reviveAnimatorTrigger = "Revive";

    /// <summary>
    /// Si el humanoide esta vivo o muerto
    /// </summary>
    /// <value></value>
    public bool Dead {get; private set;} = false;


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

        Dead = true;

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

    /// <summary>
    /// Iniciar proceso de revivir
    /// </summary>
    public virtual void StartRevive() {
        Dead = false;

        ani.SetTrigger(reviveAnimatorTrigger);
    }

    /// <summary>
    /// Completar el proceso de revivir, require haber llamado a StartRevive primero
    /// </summary>
    public virtual void CompleteRevive() {
        if (Dead)
        {
            StartRevive();
            return;
        }

        if (movement)
        {
            movement.ActiveMovement = true;
        }

        if (meleeManager)
        {
            meleeManager.ActiveMeleeCombat = true;
        }

        life.Revive();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (Dead && Input.anyKey) {
            StartRevive();
        }
    }
}