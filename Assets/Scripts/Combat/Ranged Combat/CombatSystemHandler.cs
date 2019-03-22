using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystemHandler : MonoBehaviour
{
    /// <summary>
    /// Inventario de armas melee disponibles
    /// </summary>
    [SerializeField]
    private List<MeleeWeapon> meleeWeapons;
    /// <summary>
    /// Inventario de armas de rango disponibles
    /// </summary>
    [SerializeField]
    private List<RangedWeapon> rangedWeapons;

    /// <summary>
    /// Bool que indica si se esta cambiando de arma
    /// </summary>
    private bool inChange = false;
    /// <summary>
    /// Bool que indica si se esta en el modo melee
    /// </summary>
    
    private bool inMelee;
    /// <summary>
    /// Bool que indica si se esta en el modo de rango
    /// </summary>
    
    private bool inRange;

    /// <summary>
    /// Int que indica el index del arma melee equipada en meleeWeapons
    /// </summary>
    [SerializeField]
    private int meleeEquipped = 0;
    /// <summary>
    /// Int que indica el index del arma de rango equipada en rangedWeapons
    /// </summary>
    [SerializeField]
    private int rangedEquipped = 0;

    /// <summary>
    /// Script asociado al GameObject encargado de manejar el combate de rango 
    /// </summary>
    private PlayerRangedHandler rangedSystem;
    /// <summary>
    /// Script asociado al GameObject encargado de manejar el combate melle
    /// </summary>
    private PlayerMeleeCombat meleeSystem;

    private void Awake()
    {
        rangedSystem = GetComponent<PlayerRangedHandler>();
        meleeSystem = GetComponent<PlayerMeleeCombat>();
    }

    private void Start()
    {
        //Si se quiere empezar con un arma de rango equipada
        //inMelee = false;
        //inRange = true;
        //Si se quiere empezar con un arma melee equipada
        inMelee = true;
        inRange = false;
        EquipNewWeapon();
    }

    /// <summary>
    /// Funcion para equipar una nueva arma y activa/desactiva los sistemas correspondientes
    /// </summary>
    public void EquipNewWeapon()
    {
        if (inMelee)
        {
            //Se desactiva el sistema de combate por rango
            rangedSystem.DisabeRangedCombat();
            inRange = false;

            //Se activa el sistema de combate melee
            meleeSystem.Weapon = meleeWeapons[meleeEquipped];
            meleeSystem.ActiveMeleeCombat = true;
            inMelee = true;
            
        }
        else
        {
            //Se activa el sistema de combate por rango
            rangedSystem.EnableRangedCombat();
            rangedSystem.EquippedWeapon = rangedWeapons[rangedEquipped];
            inRange = true;

            //Se desactiva el sistema de combate melee
            meleeSystem.ActiveMeleeCombat = false;
            inMelee = false;
            
        }
    }

    /// <summary>
    /// Funcion que cambia el tipo de combate
    /// </summary>
    public void ChangeCombat()
    {
        inMelee = !inMelee;
        inRange = !inRange;
        EquipNewWeapon();
    }

    /// <summary>
    /// Funcion para navegar el inventario de armas hacia adelante
    /// </summary>
    public void ChangeWeaponForward()
    {
        if (inMelee)
            if (meleeEquipped < meleeWeapons.Count - 1)
            {
                meleeEquipped += 1;
            }
            else
            {
                meleeEquipped = 0;
            }
        else
        {
            if (rangedEquipped < meleeWeapons.Count - 1)
            {
                rangedEquipped += 1;
            }
            else
            {
                rangedEquipped = 0;
            }
        }
        EquipNewWeapon();
    }

    /// <summary>
    /// Funcio para navegar el inventario de armas hacia atras
    /// </summary>
    public void ChangeWeaponBackward()
    {
        if (inMelee)
            if (meleeEquipped > 0)
            {
                meleeEquipped -= 1;
            }
            else
            {
                meleeEquipped = meleeWeapons.Count - 1;
            }
        else
        {
            if (rangedEquipped > 0)
            {
                rangedEquipped -= 1;
            }
            else
            {
                rangedEquipped = rangedWeapons.Count - 1;
            }
        }
        EquipNewWeapon();
    }

    //Preguntar si se va a poder dejar armas y cosas asi
    public void OnWeaponRemove()
    {

    }

    /// <summary>
    /// Funcion que inicia, espera, creo que ni se usa
    /// </summary>
    /// <param name="isMelee"></param>
    public void InitializeFirstWeapon(bool isMelee)
    {
        if (isMelee)
        {
            rangedSystem.DisabeRangedCombat();
            meleeSystem.Weapon = meleeWeapons[0];
            meleeSystem.ActiveMeleeCombat = true;
            inMelee = true;
            inRange = false;
        }
        else
        {
            rangedSystem.EnableRangedCombat();
            rangedSystem.EquippedWeapon = rangedWeapons[0];
            meleeSystem.ActiveMeleeCombat = false;
            inMelee = false;
            inRange = true;
        }
    }

    private void Update()
    {
        if (PlayerInput.ChangeWeapon >= 1 && !inChange)
        {
            inChange = true;
            ChangeWeaponForward();
            
        }
        else if (PlayerInput.ChangeWeapon <= -1 && !inChange)
        {
            inChange = true;
            ChangeWeaponBackward();
            
        }
        else if (PlayerInput.ChangeWeapon == 0 && inChange)
        {
            inChange = false;
        }

        if (PlayerInput.CombatType)
        {
            ChangeCombat();
        }
    }
}
