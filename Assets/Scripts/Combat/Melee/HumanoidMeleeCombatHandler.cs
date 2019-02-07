using UnityEngine;
using MoreTags;

[RequireComponent(typeof(HumanoidMovementAnimation))]
public class HumanoidMeleeCombatHandler : MonoBehaviour {

    /// <summary>
    /// Patron de los tags de gameObjecto que seran afectados por las armas melee
    /// </summary>
    [Tooltip("Patron de los tags de gameObjecto que seran afectados por las armas melee")]
    [SerializeField]
    private string targetsPattern = "Damageable.*";

    /// <summary>
    /// Tags relacionados al patron
    /// </summary>
    private string[] targetPatternInternal;

    /// <summary>
    /// Arma Actualmente selecionada
    /// </summary>
    [Tooltip("Arma Actualmente selecionada")]
    [SerializeField]
    private MeleeWeapon weapon;

    /// <summary>
    /// Arma Actualmente selecionada
    /// </summary>
    public MeleeWeapon Weapon {
        get{return weapon;} 
        set{ChangeWeapon(value);}
    }

    /// <summary>
    /// Si el combate melee esta activo
    /// </summary>
    [Tooltip("Si el combate melee esta activo")]
    [SerializeField]
    private bool activeMeleeCombat = true;

    /// <summary>
    /// Si el combate melee esta activo
    /// </summary>
    public bool ActiveMeleeCombat {
        get{return activeMeleeCombat;} 
        set{activeMeleeCombat = value;}
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        targetPatternInternal = TagUtilities.PatternToStrings(targetsPattern);
        GetComponent<HumanoidMovementAnimation>().onFlipChange.AddListener(OnFlipChange);
    }

    /// <summary>
    /// Manejar el flip de sprite
    /// </summary>
    /// <param name="isFacingRight">Si esta viendo a la derecha</param>
    void OnFlipChange(bool isFacingRight) {
        if (weapon)
        {
            Vector3 pos = weapon.gameObject.transform.localPosition;
            if ((isFacingRight && pos.x < 0) || (!isFacingRight && pos.x > 0))
            {
                pos.x *= -1.0f;
                if (weapon)
                {
                    weapon.Flip();
                }
            }
            weapon.gameObject.transform.localPosition = pos;
        }
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        HumanoidMovementAnimation hma = GetComponent<HumanoidMovementAnimation>();
        if (hma)
        {
            hma.onFlipChange.RemoveListener(OnFlipChange);
        }
    }

    /// <summary>
    /// Cambiar arma actual
    /// </summary>
    /// <param name="newWeapon">Nueva arma a poner, puede ser null</param>
    public void ChangeWeapon(MeleeWeapon newWeapon) {
        if (weapon)
        {
            weapon.gameObject.SetActive(false);
        }
        
        weapon = newWeapon;

        if (weapon)
        {
            weapon.gameObject.SetActive(false);
            weapon.SetupWeapon(targetPatternInternal);
        }
    }
}