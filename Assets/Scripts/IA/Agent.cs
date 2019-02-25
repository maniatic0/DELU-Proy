using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : HumanoidMovement {
    /// <summary>
    /// Velocidad maxima a la que ira el agente inteligente.
    /// </summary>
	public float MaxSpeed
    {
        get { return maxSpeed; }
    }

    /// <summary>
    /// Velocidad interna del agente.
    /// </summary>
	[SerializeField][Tooltip("Velocidad interna del agente.")] Vector3 mVelocity;
	
    /// <summary>
    /// Variable donde ira guardado la velocidad para usos del momento.
    /// </summary>
	private Vector3 displacement;

    /// <summary>
    /// Inicializacion de las variables.
    /// </summary>
	void Start() {
		mVelocity = Vector3.zero;
	}

    /// <summary>
    /// Funcion para actualizar la informacion del agente.
    /// </summary>
    /// <param name="nSteering">Clase Steering que tiene la informacion pasada por los Behaviours.</param>
    /// Por ahora estoy cambiando el sistema de IA para no hacer uso del Steering, pero ya veremos
	public void UpdateAgent(Vector3 nVelocity) {
		
		/// Verificacion si el agente es Kinematico o no, dependiendo de ello hace displacement la velocidad que se le pasa o la que se guarda.
		//if (nSteering.isKinematic == true) {
		//	displacement = nSteering.linear;
		//} else {
		//	displacement = mVelocity * Time.deltaTime;
		//}
        displacement = nVelocity;

		SetVelocity(displacement);

        /// <summary>
        /// Actualizar la velocidad interna del agente.
        /// </summary>
		mVelocity += nVelocity * Time.deltaTime;

        /// <summary>
        /// Si el agente no es kinematico entonces se capea la velocidad a la maxima velocidad que tenemos.
        /// </summary>
		//if (nSteering.isKinematic == false) {
		//	if(mVelocity.magnitude > MaxSpeed) {
		//		mVelocity.Normalize();
		//		mVelocity *= maxSpeed;
		//	}
        //}

        /// <summary>
        /// Si la velocidad que se le pasa es cero, se coloca que sea cero.
        /// </summary>
        /// <value></value>
		if(nVelocity.Equals(Vector3.zero)) {
			mVelocity = Vector3.zero;
		}
	}
}
