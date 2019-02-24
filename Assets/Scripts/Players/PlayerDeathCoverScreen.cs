using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerDeathCoverScreen : MonoBehaviour
{   
    /// <summary>
    /// Animador del Screen
    /// </summary>
    private Animator ani;

    /// <summary>
    /// Fade Animator Trigger
    /// </summary>
    [Tooltip("Fade Animator Trigger")]
    [SerializeField]
    private string fadeAnimatorTrigger = "Fade";

    public static PlayerDeathCoverScreen Manager {get; private set;}

    private bool fadingIn = false;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (Manager)
        {
            if (Manager != this)
            {
                Debug.LogError("DeathScreenManager Duplicado", this.gameObject);
                Destroy(this);
            }
        } else {
            Manager = this;
        }
        ani = GetComponent<Animator>();
    }

    /// <summary>
    /// Start Fading In
    /// </summary>
    public static void StartFadeIn() {
        if (Manager.fadingIn)
        {
            return;
        }
        Manager.fadingIn = true;
        Manager.ani.SetTrigger(Manager.fadeAnimatorTrigger);
    }

    /// <summary>
    /// Start Fading Out
    /// </summary>
    public static void StartFadeOut() {
        if (!Manager.fadingIn)
        {
            return;
        }
        Manager.fadingIn = false;
        Manager.ani.SetTrigger(Manager.fadeAnimatorTrigger);
    }


}
