using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public JoystickInput moveInputs;
	public JoystickInput rotateInputs;
	public CharacterController charController;

	public float moveSpeed;

	void Start( ) {

	}

	// Update is called once per frame
	void FixedUpdate( ) {
		Vector3 moveDir = new Vector3( this.moveInputs.InputDirection.x, 0, this.moveInputs.InputDirection.y );
		Vector3 lookDir = new Vector3( this.rotateInputs.InputDirection.x, 0, this.rotateInputs.InputDirection.y );

		moveDir *= this.moveSpeed * this.moveInputs.InputMagnitude;

		this.charController.SimpleMove( moveDir );
		this.charController.transform.LookAt( this.charController.transform.position + lookDir );
	}
}
