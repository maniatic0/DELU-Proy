using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedHandler : RangedCombatSystem
{    
    /// <summary>
    /// Float para indicar en que tiempo se presiono el boton de disparo
    /// </summary>
    private float pressTime;

    void Update()
    {
        if (PlayerInput.AttackDown && ReadyToShoot && ActiveRanged)
        {
            pressTime = Time.time;
        }
        if (PlayerInput.AttackUp && ReadyToShoot && ActiveRanged)
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
