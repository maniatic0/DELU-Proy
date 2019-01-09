using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	/// <summary>
	/// Velocidad maxima a la que ira el player.
	/// </summary>
	[SerializeField] float maxSpeed = 7f;

	float mov_X = 0f;
	float mov_Z = 0f;
	
	private Rigidbody rb;

	SpriteFlip spriteFlip;

	bool isFacingRight = true;

	void Awake() {
		rb = GetComponent <Rigidbody> ();
		spriteFlip = GetComponent <SpriteFlip> ();
	}

	void Update() {
		mov_X = PlayerInput.Axis.x * maxSpeed;
		mov_Z = PlayerInput.Axis.y * maxSpeed;
	}
	void FixedUpdate() {
		Move();
		Flip();
	}

	void Move() {
		rb.velocity = new Vector3(mov_X, 0f, mov_Z);
		
	}

	void Flip() {
		if(rb.velocity.x < 0 && isFacingRight) {
            spriteFlip.FlipX = true;
            isFacingRight = false;
        } else if(rb.velocity.x > 0 && !isFacingRight) {
            spriteFlip.FlipX = false;
            isFacingRight = true;
        }
	}

}
