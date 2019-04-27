using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MoreTags;

public class WeaponChange : UnityEvent{};

public class RangedSystem : MonoBehaviour
{
    /// <summary> Pattern de objetos que pueden recibir dano </summary>
    [SerializeField]
    private readonly string damageablePattern = "Damageable.*";
    /// <summary> Pattern de objetos a los que se le puede disparar </summary>
    [SerializeField]
    private readonly string hittablePattern = "Hittable.*";

    /// <summary> Indica si el sistema esta activo </summary>
    public bool ActiveRanged
    {
        get { return activeRange; }
    }
    /// <summary> Indica si el sistema esta activo </summary>
    private bool activeRange = false;

    /// <summary> Arma equipada </summary>
    public RangedWeaponSO EquippedWeapon
    {
        get { return weaponEquipped; }
        set { ChangeWeapon(value); }
    }

    /// <summary> Arma equipada </summary>
    private RangedWeaponSO weaponEquipped = null;

    /// <summary> Arma de projectil </summary>
    private ProjectileWeapon pjWeapon;
    /// <summary> Arma continua </summary>
    private ContinuousWeapon cWeapon;

    private WeaponChange onRangeChange;

    protected virtual void Awake()
    {
        pjWeapon = GetComponentInChildren<ProjectileWeapon>();
        cWeapon = GetComponentInChildren<ContinuousWeapon>();
        GetComponent<HumanoidMovementAnimation>().onFlipChange.AddListener(OnFlip);
    }

    void OnFlip(bool isFacingRight)
    {
        pjWeapon.Flip(isFacingRight);
        cWeapon.Flip(isFacingRight);
    }

    /// <summary> Dispara el arma equipada </summary>
    /// <param name="direction">Direccion del disparo</param>
    /// <param name="target">Objetivo del disparo</param>
    protected void ShootRanged(Vector3 direction, bool isCharged = false, Transform target = null)
    {
        if (weaponEquipped.GetType() == typeof(ProjectileWeaponSO))
        {
            if (!isCharged)
            {
                Debug.Log("Disparando PJ no cargado");
                pjWeapon.Shoot(direction, target);
            }
            else
            {
                Debug.Log("Disparando PJ cargado");
                pjWeapon.ChargedShoot(direction, target);
            }
        }
        else
        {
            cWeapon.Shoot(target);
        }
    }

    /// <summary> Desactiva el combate de rango </summary>
    public void DisableRangedCombat()
    {
        activeRange = false;
        if (pjWeapon.gameObject.activeInHierarchy) pjWeapon.gameObject.SetActive(false);
        if (cWeapon.gameObject.activeInHierarchy) cWeapon.gameObject.SetActive(false);
    }

    /// <summary> Activa el combate de rango </summary>
    public void EnableRangedCombat()
    {
        activeRange = true;

    }

    /// <summary> Cambia el arma de rango equipada </summary>
    /// <param name="newWeapon">Arma a equipar</param>
    void ChangeWeapon(RangedWeaponSO newWeapon)
    {
        weaponEquipped = newWeapon;
        if (weaponEquipped == null)
        {
            pjWeapon.gameObject.SetActive(false);
            cWeapon.gameObject.SetActive(false);
            if (pjWeapon.gameObject.activeInHierarchy) pjWeapon.gameObject.SetActive(false);
            if (cWeapon.gameObject.activeInHierarchy) cWeapon.gameObject.SetActive(false);
        }

        else if (weaponEquipped.GetType() == typeof(ProjectileWeaponSO))
        {
            pjWeapon.InitializeWeapon(weaponEquipped as ProjectileWeaponSO);
            if (!pjWeapon.gameObject.activeInHierarchy) pjWeapon.gameObject.SetActive(true);
            if (cWeapon.gameObject.activeInHierarchy) cWeapon.gameObject.SetActive(false);
        }

        else if (weaponEquipped.GetType() == typeof(ContinuousWeaponSO))
        {
            cWeapon.InitializeWeapon(weaponEquipped as ContinuousWeaponSO);          
            if (pjWeapon.gameObject.activeInHierarchy) pjWeapon.gameObject.SetActive(false);
            if (!cWeapon.gameObject.activeInHierarchy) cWeapon.gameObject.SetActive(true);
        }
    }   
}
