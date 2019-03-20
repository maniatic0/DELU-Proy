using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ranged Weapon", menuName = "Ranged Weapon")]
public class RangedWeapon : ScriptableObject
{
    //Deberia llamarse RangedWeapon pero ya hay un script llamado asi
    //Pero ese script no sirve sino para debuggin jajaja, solo me da flojera
    //cambiarlo... asi que en algun momento CAMBIARLO Xd
    [Tooltip("Tipo de dano")]
    public EffectType damageType;
    [Tooltip("Dano del efecto")]
    public float damage;
    [Tooltip("Cool Down del disparo")]
    public float fireRate;
    [Tooltip("Tiempo en cargar el ataque")]
    public float chargeTime = 1.25f;
    [Tooltip("Bonus de dano (1 = damage, 1.x = damage + x porciento)")]
    public float chargeBonus = 1.25f;

    public Sprite weaponSprite;
    public Sprite projectileSprite;

}
