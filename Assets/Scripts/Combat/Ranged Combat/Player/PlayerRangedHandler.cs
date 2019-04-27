using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RangeChange : UnityEvent { }

public class PlayerRangedHandler : RangedSystem
{    

    //Para evitar hacer tantos raycast por frame, se puede hacer que en el rayfromcamera dispare solo
    //en cada intervalo de dano del arma o hasta se puede integrar eso mas al scriptable object y que el arma
    //solo reciba el a hacer.

    /// <summary>
    /// Float para indicar en que tiempo se presiono el boton de disparo
    /// </summary>
    private float pressTime;

    Camera cam;

    protected override void Awake()
    {
        base.Awake();
        cam = Camera.main;
    }

    void Update()
    {
        if (ActiveRanged && EquippedWeapon != null)
        {
            if (EquippedWeapon.GetType() == typeof(ProjectileWeaponSO))
            {
                if (PlayerInput.AttackDown)
                {
                    pressTime = Time.time;
                }
                if (PlayerInput.AttackUp)
                {
                    if (Time.time - pressTime >= (EquippedWeapon as ProjectileWeaponSO).chargeTime)
                    {
                        //Debug.Log("Cargado!");
                        ShootRanged(RayFromScreenDir(PlayerInput.MousePosition), true);
                    }
                    else
                    {
                        //Debug.Log("No cargado");
                        ShootRanged(RayFromScreenDir(PlayerInput.MousePosition));
                    }
                }
            }
            else
            {
                if (PlayerInput.Attack)
                {
                    //Vector3 zero o lo que sea
                    ShootRanged(Vector3.zero, false, RayFromScreenTrans(PlayerInput.MousePosition));
                }
            }
        }
    }

    /// <summary> Dispara un raycast de la camara al mouse </summary>
    /// <param name="mouseClick">Posicion del mouse al clickear</param>
    /// <returns>Direccion del jugador al click</returns>
    Vector3 RayFromScreenDir(Vector3 mouseClick)
    {
        Ray ray = cam.ScreenPointToRay(mouseClick);
        RaycastHit hit;

        Debug.DrawLine(ray.origin, ray.direction * 10, Color.red, 5);

        //Deberia hacer que el raycast solo interactue con una layer de colliders?
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 direction = (hit.point - transform.position).normalized;
            return direction;
            //ShootRanged(direction, hit.transform);
        }
        return Vector3.right;
    }

    /// <summary> Dispara un raycast de la camara al mouse </summary>
    /// <param name="mouseClick">Posicion del mouse al clickear</param>
    /// <returns>Transform del objeto clickeado</returns>
    Transform RayFromScreenTrans(Vector3 mouseClick)
    {
        Ray ray = cam.ScreenPointToRay(mouseClick);
        RaycastHit hit;

        Debug.DrawLine(ray.origin, ray.direction * 10, Color.red, 5);

        //Deberia hacer que el raycast solo interactue con una layer de colliders?
        if (Physics.Raycast(ray, out hit))
        {
            return hit.transform;
        }
        return null;
    }
}
