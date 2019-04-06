using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectile : MonoBehaviour
{
    /// <summary>
    /// Velocidad a la que se movera el proyectil
    /// </summary>
    public float speed = 15f;

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
    public EffectType damageType;
    /// <summary>
    /// NO SE, CHRISTIAN QUE ES ESTO
    /// </summary>
    private EffectInfo info;
    /// <summary>
    /// Dano del proyectil
    /// </summary>
    public float damage;
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
    public void UpdateSprite(Sprite spr)
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
    public void InitializeProjectile(RangedWeapon weapon, Transform spawner, GameObject originObject)
    {     
        if(weapon.projectileSprite != VFX_SR.sprite)
        {
            UpdateSprite(weapon.projectileSprite);
        }

        if(spawner != shotSpawn)
        {
            shotSpawn = spawner;
        }

        if(weapon.damageType != damageType)
        {
            damageType = weapon.damageType;
        }

        if(weapon.damage != damage)
        {
            damage = weapon.damage;
        }
        shooter = originObject;
        info = new EffectInfo(damageType, gameObject);
    }

    /// <summary>
    /// Funcion para cambiar la rotacion de un proyectil dependiendo de su objetivo
    /// </summary>
    public void ChangeRotation()
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
        Quaternion desiredRotation = Quaternion.LookRotation(forwardVector,upVector);
        transform.rotation = desiredRotation;
    }


    /// <summary>
    /// Funcion cuando se dispara a un objetivo
    /// </summary>
    /// <param name="newTarget">Objetivo</param>
    /// <param name="buff">Bufo de dano</param>
    public void ShootAtTarget(Transform newTarget, float buff = 1f)
    {
        target = newTarget;
        isTarget = true;
        //Debug.Log("player: " + shotSpawn.position + " enemy: " + target.position);
        direction = target.position - shotSpawn.position;
        //Debug.Log("direction: " + direction);
        transform.position = shotSpawn.position;
        damageBuff = buff;
        ChangeRotation();
    }

    /// <summary>
    /// Funcion usado cuando no se dispara a un objetivo
    /// </summary>
    /// <param name="hitPoint">A que punto se dispara</param>
    public void ShootAtNothing(Vector3 hitPoint)
    {
        isTarget = false;
        //Debug.Log("player: " + shotSpawn.position + " spot: " + hitPoint);
        //En este caso, el valor de y es arbitrario, es para mantener una distancia del suelo constante en todos los disparos.
        //direction = new Vector3(hitPoint.x, 1.2f, hitPoint.z) - shotSpawn.position;
        //Tambien se puede hacer que dispare donde sea
        direction = hitPoint - shotSpawn.position;
        //Debug.Log("direction: " + direction);
        transform.position = shotSpawn.position;
        ChangeRotation();
        //En el tiempo se debe de poner cuanto tiempo durara la flecha en el escenario.
        //Invoke("ProjectileDestroy", 1f);
    }

    /// <summary>
    /// Funcion que le aplica dano al objetivo
    /// </summary>
    public void DoDamage(float damageBuff = 1f)
    {
        GameObject objective = target.gameObject;
        LifeManager lifeManager = objective.GetComponent<LifeManager>();
        lifeManager.ApplyChange(info.StartTimeCount(damageBuff));
    }

    /// <summary>
    /// Funcion para reiniciar los projectiles disparados
    /// </summary>
    public void ProjectileDestroy()
    {
        direction = Vector3.zero;
        VFX.transform.rotation = Quaternion.identity;
        damageBuff = 1f;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (isTarget)
        {
            //Distancia entre el proyectil y el objetivo
            float distance = Mathf.Abs(Vector3.Magnitude(target.position - transform.position));
            //Debug.Log(distance + " speed: " + speed);
            //El proyectil sigue su velocidad normal
            if (speed * Time.deltaTime < distance)
            {
                transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
            }
            //El proyectil para en el objetivo
            else
            {
                transform.Translate(direction.normalized * distance * Time.deltaTime, Space.World);
                DoDamage(damageBuff);
                ProjectileDestroy();
            }
        }
        //En caso de que no haya un objetivo, solo sigue la direccion
        else
        {
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);           
        }
    }
}