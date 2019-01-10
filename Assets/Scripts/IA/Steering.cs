using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering {
    /// <summary>
    /// Velocidad lineal del agente.
    /// </summary>
	public Vector3 linear;

    /// <summary>
    /// Booleano que indica si es kinematico o no.
    /// </summary>
	public bool isKinematic;

    /// <summary>
    /// Constructor del steering
    /// </summary>
	public Steering(bool kinematic) {
		linear = Vector3.zero;
		isKinematic = kinematic;
	}
}
