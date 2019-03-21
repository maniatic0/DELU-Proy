using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedHandler : RangedCombatSystem
{    
    /// <summary>
    /// Float para indicar en que tiempo se presiono el boton de disparo
    /// </summary>
    private float pressTime;

    public RangedWeapon altWeaponTest;

    void Update()
    {
        if (PlayerInput.AttackDown && ReadyToShoot)
        {
            pressTime = Time.time;
        }
        if (PlayerInput.AttackUp && ReadyToShoot)
        {
            if (Time.time - pressTime >= EquippedWeapon.chargeTime)
            {
                Debug.Log("Cargado!");
                ShootRay(damageBuff: EquippedWeapon.chargeBonus);
            }
            else
            {
                Debug.Log("No cargado");
                ShootRay();
            }
        }
    }
}
