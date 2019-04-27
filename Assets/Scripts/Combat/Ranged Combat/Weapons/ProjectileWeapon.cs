using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : MonoBehaviour
{
    public EffectType DamageType
    {
        get { return damageType; }
    }
    /// <summary> Efecto a aplicar con el arma </summary>
    private EffectType damageType;
    /// <summary> Daño del arma </summary>
    private float damage = 1f;
    /// <summary> Tiempo entre cada disparo </summary>
    private float fireRate = 1.2f;
    /// <summary> Tiempo de carga del disparo </summary>
    private float chargeTime = 1.25f;
    /// <summary> Bonus de daño por carga </summary>
    private float chargeBonus = 1.5f;
    /// <summary> Estado de recarga del arma </summary>
    private bool reloading = false;

    /// <summary> Sprite del arma </summary>
    private Sprite weaponSprite;
    public Sprite ProjectileSprite
    {
        get { return projectileSprite; }
    }
    /// <summary> Sprite del proyectil </summary>
    private Sprite projectileSprite;
    /// <summary> Renderer del arma </summary>
    private SpriteRenderer spriteRenderer;

    //[SerializeField]
    /// <summary> Pool de proyectiles</summary>
    private ProjectilePool projectilePool;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //ES NECESARIO QUE EXISTA UN GAMEOBJECT POOL MANAGER CON LASP OOLS
        projectilePool = GameObject.Find("Pool Manager").GetComponent<ProjectilePool>();
    }

    /// <summary> Actualiza los datos del arma </summary>
    /// <param name="weapon">Arma a inicializar</param>
    public void InitializeWeapon(ProjectileWeaponSO weapon)
    {
        if (weapon == null)
        {
            throw new System.NullReferenceException();
        }
        damageType = weapon.damageType;
        damage = weapon.damage;
        fireRate = weapon.fireRate;
        chargeTime = weapon.chargeTime;
        chargeBonus = weapon.chargeBonus;
        GetComponent<SpriteRenderer>().sprite = weapon.weaponSprite;
        weaponSprite = weapon.weaponSprite;
        projectileSprite = weapon.projectileSprite;
    }

    /// <summary> Rota el sprite y mueve su posicion dependiendo de donde mire el usuario </summary>
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

    /// <summary> Dispara un proyectil no cargado </summary>
    /// <param name="direction">Direccion en la que se dispara</param>
    /// <param name="target"></param>
    public void Shoot(Vector3 direction, Transform target = null)
    {
        if (!reloading)
        {
            GameObject projectile = projectilePool.GetFromPool();
            SimpleProjectile projectileScript = projectile.GetComponent<SimpleProjectile>();
            projectileScript.InitializeProjectile(this, transform, transform.parent.gameObject);
            projectileScript.ShootProjectile(direction);
            StartCoroutine(Reload(fireRate));
        }
    }

    /// <summary> Dispara un proyectil cargado </summary>
    /// <param name="direction">Direccion en la que se dispara</param>
    /// <param name="target"></param>
    public void ChargedShoot(Vector3 direction, Transform target = null)
    {
        if (!reloading)
        {
            GameObject projectile = projectilePool.GetFromPool();
            SimpleProjectile projectileScript = projectile.GetComponent<SimpleProjectile>();
            projectileScript.InitializeProjectile(this, transform, transform.parent.gameObject, chargeBonus);
            projectileScript.ShootProjectile(direction);
            StartCoroutine(Reload(fireRate));
        }
    }

    /// <summary> Recarga el arma </summary>
    /// <param name="reloadTime">Tiempo de recarga</param>
    IEnumerator Reload(float reloadTime)
    {
        reloading = true;
        yield return new WaitForSeconds(reloadTime);
        reloading = false;
    }
}
