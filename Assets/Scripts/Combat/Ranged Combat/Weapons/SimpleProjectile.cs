using MoreTags;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectile : MonoBehaviour
{
    /// <summary>
    /// Velocidad a la que se movera el proyectil
    /// </summary>
    [SerializeField]
    private float speed = 15f;

    /// <summary>
    /// Tiempo que durara el projectil en el escenario
    /// en caso de que no colisione
    /// </summary>
    [SerializeField]
    private float lifeTime = 3f;

    /// <summary>
    /// Patron de los tags de GameObjects afectados por los projectiles
    /// </summary>
    [SerializeField]
    private string targetsPattern = "Damageable.*";

    /// <summary>
    /// Indica si el proyectil tiene un objetivo
    /// </summary>
    private bool isTarget;

    /// <summary>
    /// Direccion del proyectil
    /// </summary>
    private Vector3 direction;
    /// <summary>
    /// GameObject asociado al sprite
    /// </summary>
    private GameObject VFX;
    /// <summary>
    /// Sprite Renderer del VFX
    /// </summary>
    private SpriteRenderer VFX_SR;
    /// <summary>
    /// Objetivo (en caso de que haya uno)
    /// </summary>
    public Transform target;
    /// <summary>
    /// De donde es disparado el proyectil
    /// </summary>
    [SerializeField]
    private Transform shotSpawn;
    /// <summary>
    /// GameObject del player/AI que esta disparando
    /// </summary>
    private GameObject shooter;

    /// <summary>
    /// Tipo de dano del proyectil
    /// </summary>
    private EffectType damageType;
    /// <summary>
    /// NO SE, CHRISTIAN QUE ES ESTO
    /// </summary>
    private EffectInfo info;
    /// <summary>
    /// Bufo de dano del proyectil
    /// </summary>
    private float damageBuff = 1f;

    private void Awake()
    {
        VFX = transform.Find("VFX").gameObject;
        VFX_SR = VFX.GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Funcion para updatear el sprite de un proyectil
    /// </summary>
    /// <param name="spr">Nuevo sprite</param>
    void UpdateSprite(Sprite spr)
    {
        VFX_SR.sprite = spr;
    }

    /// <summary>
    /// Funcion para verificar si hay algun dato del proyectil que cambiar.
    /// El efecto es notable cuando hay un cambio de arma.
    /// </summary>
    /// <param name="weapon">Arma de rango originaria del dano</param>
    /// <param name="spawner">De donde sale el proyectil</param>
    /// <param name="originObject">Quien dispara el proyectil</param>
    public void InitializeProjectile(ProjectileWeapon weapon, Transform spawner, GameObject originObject, float dmgBuff = 1f)
    {
        if (weapon.ProjectileSprite != VFX_SR.sprite)
        {
            Debug.Log("Updating projectile sprite!");
            UpdateSprite(weapon.ProjectileSprite);
        }   
        if (spawner != shotSpawn)
        {
            shotSpawn = spawner;
        }    
        if (weapon.DamageType != damageType)
        {
            damageType = weapon.DamageType;
        }
        damageBuff = dmgBuff;

        shooter = originObject;
        Physics.IgnoreCollision(GetComponent<Collider>(), shooter.GetComponent<Collider>(), true);
        info = new EffectInfo(damageType, gameObject);
    }

    /// <summary>
    /// Funcion para cambiar la rotacion de un proyectil dependiendo de su objetivo
    /// </summary>
    void ChangeRotation()
    {
        bool flipped = shooter.GetComponent<SpriteFlip>().FlipX;
        //Producto cruz loco que calcula el angulo
        //float angle = Vector3.Angle(Vector3.right, new Vector3(direction.x, 0, direction.z));
        //Vector3 cross = Vector3.Cross(Vector3.right, new Vector3(direction.x, 0, direction.z));
        //angle *= (cross.y < 0) ? -1 : 1;
        //VFX.transform.Rotate(Vector3.up * angle);
    
        Vector2 upVector = Vector2.Perpendicular(new Vector2(direction.x, direction.y));
        Vector3 forwardVector;
        if (!flipped)
        {
            forwardVector = Vector3.Cross(direction, Vector3.up);
        }
        else
        {
            forwardVector = Vector3.Cross(direction, Vector3.down);
        }
        Quaternion desiredRotation = Quaternion.LookRotation(forwardVector, upVector);
        transform.rotation = desiredRotation;
    }

    /// <summary> Metodo usado para disparar el projectil </summary>
    public void ShootProjectile(Vector3 projDirection)
    {
        isTarget = false;
        target = null;
        direction = projDirection;
        transform.position = shotSpawn.position;

        ChangeRotation();
        //StartCoroutine(DestroyTimer(lifeTime));
        //En el tiempo se debe de poner cuanto tiempo durara la flecha en el escenario.
    }

    /// <summary> Funcion que le aplica dano al objetivo </summary>
    void DoDamage(Transform hitted, float damageBuff = 1f)
    {
        Debug.Log(hitted.name);
        if (hitted.gameObject.AnyTags(TagUtilities.PatternToStrings(targetsPattern)))
        {
            //Debug.Log("Target is damagable.");
            GameObject objective = hitted.gameObject;
            LifeManager lifeManager = objective.GetComponent<LifeManager>();
            //Creo que deberia ser weapon damage + damage*porcentaje dado a que para no tener que crear
            //Un monton de efectos, se usa uno de dano base con dano 1
            lifeManager.ApplyChange(info.StartTimeCount(damageBuff));
        }
    }

    /// <summary>
    /// Funcion para reiniciar los projectiles disparados
    /// </summary>
    void DeInitializeProjectile()
    {
        direction = Vector3.zero;
        gameObject.transform.rotation = Quaternion.identity;
        VFX.transform.rotation = Quaternion.identity;
        damageBuff = 1f;
        Physics.IgnoreCollision(GetComponent<Collider>(), shooter.GetComponent<Collider>(), false);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Timer que destruye el projectil despues de pasado un tiempo.
    /// </summary>
    /// <param name="time">Vida del projectil</param>
    /// <returns></returns>
    IEnumerator DestroyTimer(float time)
    {
        yield return new WaitForSeconds(time);
        DeInitializeProjectile();
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
    }

    void OnCollisionEnter(Collision collision)
    {
        StopCoroutine(DestroyTimer(lifeTime));
        if (collision.gameObject.layer == 8)
        {
            DoDamage(collision.transform, damageBuff);
            DeInitializeProjectile();
        }
    }
}