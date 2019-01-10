using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KSeek : Behaviours {
    public override Steering GetSteering() {
		steering.linear = target.transform.position - this.transform.position;
		steering.linear.Normalize();
		steering.linear *= agent.MaxSpeed;

		return steering;
	}

	void Update() {
		agent.UpdateAgent(GetSteering());
	}
}
