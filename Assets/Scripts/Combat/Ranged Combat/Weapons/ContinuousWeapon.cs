using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreTags;

public class ContinuousWeapon : MonoBehaviour
{
    /// <summary> Pattern de tags afectables por el arma </summary>
    [SerializeField]
    private string targetsPattern = "Damageable.*";
    /// <summary> Efecto del arma </summary>
    private EffectType damageType;
    private EffectInfo effectInfo;

    /// <summary> dps del arma</summary>
    private float dps = 0.5f;
    /// <summary> Intervalo de tiempo en el que se aplicara el daño </summary>
    [SerializeField]
    private float damageIntervalTime;
    /// <summary> Daño que se aplicara en cada intervalo de daño </summary>
    private float intervalDamage;

    /// <summary> Sprite del arma </summary>
    private Sprite weaponSprite;
    /// <summary> Sprite del rayo </summary>
    private Sprite raySprite;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary> Actualiza los datos del arma </summary>
    /// <param name="weapon">Arma a inicializar</param>
    public void InitializeWeapon(ContinuousWeaponSO weapon)
    {
        Debug.Log("Inicializando continuous.");
        if (weapon == null)
        {
            throw new System.NullReferenceException();
        }
        damageType = weapon.damageType;
        dps = weapon.damage;
        spriteRenderer.sprite = weapon.weaponSprite;
        weaponSprite = weapon.weaponSprite;
        raySprite = weapon.raySprite;
        effectInfo = new EffectInfo(damageType, gameObject);
        CalculateSomething(dps);
    }

    /// <summary> Rota el sprite y mueve su posicion dependiendo de donde mire el usuario</summary>
    /// <param name="isFacingRight">Si el usuario mira a la derecha</param>
    public void Flip(bool isFacingRight)
    {
        spriteRenderer.flipX = !isFacingRight;
        if (isFacingRight)
        {
            transform.localPosition = Vector3.right;
        }
        else
        {
            transform.localPosition = Vector3.left;
        }
    }

    /// <summary> Dispara el arma </summary>
    /// <param name="target">Objectivo a disparar</param>
    public void Shoot(Transform target = null)
    {
        //Debug.Log("Disparandito continuous.");
        if (target != null)
        {
            DoDamage(target);
        }
    }

    /// <summary> Calcula el daño que debe aplicarse en cada intervalo de daño </summary>
    /// <param name="dmgps">Daño por segundo del arma</param>
    public void CalculateSomething(float dmgps)
    {
        if (damageIntervalTime > 1)
        {
            Debug.LogError("Intervalos en el que se aplicara el dano deberia de ser <= 1 segundo.");
            return;
        }
        //Debug.Log(intervalDamage);
        intervalDamage = damageIntervalTime * dps;
    }

    /// <summary> Aplica el efecto del arma al objetivo </summary>
    /// <param name="hitted">Objetivo</param>
    void DoDamage(Transform hitted)
    {
        //Debug.Log(hitted.name);
        if (hitted.gameObject.AnyTags(TagUtilities.PatternToStrings(targetsPattern)))
        {
            //Debug.Log("Target is damagable.");
            GameObject objective = hitted.gameObject;
            LifeManager lifeManager = objective.GetComponent<LifeManager>();
            //Creo que deberia ser weapon damage + damage*porcentaje dado a que para no tener que crear
            //Un monton de efectos, se usa uno de dano base con dano 1
            lifeManager.ApplyChange(effectInfo.StartTimeCount(intervalDamage));
        }
    }
}
