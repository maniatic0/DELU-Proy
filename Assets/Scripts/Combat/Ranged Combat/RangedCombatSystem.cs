using MoreTags;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectilePool))]
//[RequireComponent(typeof(RangedWeapon))]
public class RangedCombatSystem : MonoBehaviour
{
    //IDEAS//
    //-En algun script/clase/alguna estructura de datos del jugador, se puede hacer una lista
    // de los efectos que tiene el jugador. Hacer un efecto que afecte el fireRate

    /// <summary>
    /// Patron de los tags de GameObjects danables
    /// </summary>
    [SerializeField]
    private string targetsPattern = "Damageable.*";

    /// <summary>
    /// Cooldown del disparo
    /// </summary>
    private float coolDown;
    /// <summary>
    /// Indica si ya paso el tiempo del cooldown
    /// </summary>
    private bool readyToShoot = true;
    /// <summary>
    /// Indica si ya paso el tiempo del cooldown
    /// </summary>
    public bool ReadyToShoot
    {
        get {return readyToShoot;}
    }
    
    /// <summary>
    /// Arma equipada
    /// </summary>
    [SerializeField]
    private RangedWeapon equippedWeapon;
    /// <summary>
    /// Arma equipada
    /// </summary>
    public RangedWeapon EquippedWeapon
    {
        get {return equippedWeapon;}
        set {ChangeWeapon(value);}
    }
    /// <summary>
    /// De donde sale el disparo
    /// </summary>
    [SerializeField]
    private Transform shotSpawn;
    /// <summary>
    /// GameObject del VFX del arma
    /// </summary>
    [SerializeField]
    private GameObject rangedWeaponObj;

    /// <summary>
    /// Indica si el sistema de combate de rango esta activo
    /// </summary>
    [SerializeField]
    private bool activeRanged = false;

    /// <summary>
    /// Indica si el sistema de combate de rango esta activo
    /// </summary>
    public bool ActiveRanged
    {
        get { return activeRanged; }
        set { activeRanged = value; }
    }

    /// <summary>
    /// Pool de proyectiles
    /// </summary>
    private ProjectilePool pool;
    /// <summary>
    /// Camara del juego
    /// </summary>
    private Camera cam;

    public Transform dummy;

    private void Awake()
    {
        cam = Camera.main;
        pool = GetComponent<ProjectilePool>();
    }

    //Deberia empezar con un parametro de arma?
    /// <summary>
    /// Funcion que activa todo lo necesario para usar el sistema de combate por rango
    /// </summary>
    public void EnableRangedCombat()
    {
        activeRanged = true;
        UpdateWeaponSprite();
        rangedWeaponObj.SetActive(true);
    }

    /// <summary>
    /// Funcion que desactiva el sistema de combate por rango
    /// </summary>
    public void DisabeRangedCombat()
    {
        activeRanged = false;
        rangedWeaponObj.SetActive(false);
    }

    /// <summary>
    /// Funcion encargada de recargar el arma
    /// </summary>
    /// <returns></returns>
    IEnumerator ReloadBow()
    {
        //Debug.Log("Reloading...");
        readyToShoot = false;
        yield return new WaitForSeconds(coolDown);
        readyToShoot = true;
        //Debug.Log("Reloaded!");
    }

    /// <summary>
    /// Funcion para cambiar el arma equipada
    /// </summary>
    /// <param name="newWeapon">Arma a equipar</param>
    public void ChangeWeapon(RangedWeapon newWeapon)
    {
        equippedWeapon = newWeapon;
        coolDown = equippedWeapon.fireRate;
    }

    /// <summary>
    /// Funcion encargada de disparar un raycast y detectar si se dio al enemigo.
    /// Despues de esto, se dispara el proyectil.
    /// </summary>
    protected void ShootRay(float damageBuff = 1f)
    {
        //Raycast de la camara al mouse
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawLine(ray.origin, ray.direction * 10, Color.red, 5);

        //Solo se dispara si se le da a algo
        if (Physics.Raycast(ray, out hit))
        {
            //Direccion del disparo. Chequear porque puse la coordenada Y como 0
            Vector3 direction = new Vector3(hit.point.x, 0, hit.point.z) - rangedWeaponObj.transform.position;
            //Se obtiene el GameObject del proyectil, desde la pool y luego se inicializa
            GameObject projectile = pool.GetFromPool();
            //SimpleProjectile projectileControl = projectile.GetComponent<SimpleProjectile>();
            SimpleProjectile projectileControl = projectile.GetComponent<SimpleProjectile>();
            projectileControl.InitializeProjectile(weapon: equippedWeapon, spawner: shotSpawn, originObject: gameObject);
            projectile.SetActive(true);
            //En caso de que se le haya dado a algo apuntable
            if (hit.collider.gameObject.AnyTags(TagUtilities.PatternToStrings(targetsPattern)))
            {
                projectileControl.ShootAtTarget(hit.transform, damageBuff);
                //Debug.Log("Target Hit");
                DebugRay(direction, true);
            }
            //Si no se le da a nada
            else
            {
                projectileControl.ShootAtNothing(hit.point);
                //Debug.Log("Nothing Hit");
                DebugRay(direction, false);
            }
            StartCoroutine(ReloadBow());
        }
    }

    /// <summary>
    /// Esta funcion dispara a un objetivo sin posibilidad de fallar (para IA)
    /// </summary>
    /// <param name="target">Objetivo a disparar</param>
    /// <param name="damageBuff">Bufo de dano</param>
    public void ShootTarget(Transform target, float damageBuff = 1f)
    {
        GameObject projectile = pool.GetFromPool();
        //SimpleProjectile projectileControl = projectile.GetComponent<SimpleProjectile>();
        SimpleProjectile projectileControl = projectile.GetComponent<SimpleProjectile>();
        projectileControl.InitializeProjectile(weapon: equippedWeapon, spawner: shotSpawn, originObject: gameObject);
        projectile.SetActive(true);
        projectileControl.ShootAtTarget(target, damageBuff);
        StartCoroutine(ReloadBow());
        DebugRay(target.position - transform.position, true);
    }

    public void UpdateWeaponSprite()
    {
        //projectileControl.UpdateSprite(equippedWeapon.projectileSprite);
        //Cambiar el del arma cuando se implemente
    }

    /// <summary>
    /// Funcion Debug para ver en que direccion sale el proyectil disparado
    /// </summary>
    /// <param name="direction">Direccion del proyectil</param>
    /// <param name="target">Hay un objetivo o no</param>
    public void DebugRay(Vector3 direction, bool target)
    {
        if (target)
        {
            Debug.DrawRay(transform.position, new Vector3(direction.x, transform.position.y, direction.z), Color.black, 5f);
            return;
        }
        //Sino, salir disparado en la direccion del click y ya hasta que algo pase y destruya el objeto
        Debug.DrawRay(transform.position, new Vector3(direction.x, direction.y, direction.z), Color.black, 5f);
    }

    protected void DebugShoot(Transform target)
    {
        ShootTarget(target);
    }
}
