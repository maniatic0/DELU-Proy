using UnityEngine;

public class PlayerMeleeCombat : HumanoidMeleeCombatHandler {

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (PlayerInput.AttackDown)
        {
            StartAttack();
        }
    }
}