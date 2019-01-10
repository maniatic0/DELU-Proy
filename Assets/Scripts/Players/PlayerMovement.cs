using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : HumanoidMovement {

	void Update() {
		SetVelocity(PlayerInput.Axis.x * maxSpeed, 0.0f, PlayerInput.Axis.y * maxSpeed);
	}
	
}
