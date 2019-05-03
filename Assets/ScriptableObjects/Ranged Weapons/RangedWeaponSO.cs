using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ranged Weapon", menuName = "Ranged Weapon")]
public abstract class RangedWeaponSO : ScriptableObject
{
    [Tooltip("Tipo de dano")]
    public EffectType damageType;
    [Tooltip("Dano del efecto")]
    public float damage;

    public Sprite weaponSprite;

    public abstract void InitializeWeapon(GameObject user);
}
