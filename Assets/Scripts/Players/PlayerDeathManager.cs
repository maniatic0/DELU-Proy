using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathManager : HumanoidDeathManager
{

    /// <summary>
    /// Handle Death Event
    /// </summary>
    /// <param name="lm"></param>
    public override void OnDeath(LifeManager lm) {
        if (Dead)
        {
            return;
        }
        base.OnDeath(lm);

        StartCoroutine(WaitToRevive());
    }

    /// <summary>
    /// Espera para revivir a jugador
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitToRevive() {
        PlayerDeathCoverScreen.StartFadeIn();
        yield return new WaitUntil(() => Input.anyKeyDown);
        StartRevive();
        PlayerDeathCoverScreen.StartFadeOut();
    }

}
