using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile Weapon", menuName = "Ranged Weapon/Projectile Weapon")]
public class ProjectileWeaponSO : RangedWeaponSO
{
    [Tooltip("Cool Down del disparo")]
    public float fireRate = 1f;
    [Tooltip("Tiempo en cargar el ataque")]
    public float chargeTime = 1.25f;
    [Tooltip("Bonus de dano (1 = damage, 1.x = damage + x porciento)")]
    public float chargeBonus = 1.25f;

    [Tooltip("Sprite del proyectil")]
    public Sprite projectileSprite;

    private ProjectileWeapon weapon;

    public override void InitializeWeapon(GameObject user)
    {

    }
}
