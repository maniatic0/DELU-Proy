using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile Weapon", menuName = "Ranged Weapon/Continuous Weapon")]
public class ContinuousWeaponSO : RangedWeaponSO
{
    [Tooltip("Sprite del rayo continuo")]
    public Sprite raySprite;

    private ContinuousWeapon weapon;

    public override void InitializeWeapon(GameObject user)
    {
    }

    public void EffectParticles()
    {

    }
}
