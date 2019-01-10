using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviours : MonoBehaviour {

    /// <summary>
    /// Target que el agente estara siguiendo.
    /// </summary>
	protected GameObject target;

    /// <summary>
    /// Referencia a si mismo para facilidad de escritura.
    /// </summary>
	protected GameObject character;

    /// <summary>
    /// Referencia al script de Agent que maneja las fisicas del agente.
    /// </summary>
	protected Agent agent;

    /// <summary>
    /// Referencia al script de Steering que pasa la informacion del agente.
    /// </summary>
	protected Steering steering;

	void Start() {
		Init();
	}

    /// <summary>
    /// Funcion para inicializacion de las variables de los behaviours
    /// </summary>
	public virtual void Init() {
        target = GameObject.FindGameObjectWithTag("Player");
		steering = new Steering(true);
		character = this.gameObject;
		agent = GetComponent<Agent> ();
	}

	public virtual Steering GetSteering() {
		return steering;
	}
}
