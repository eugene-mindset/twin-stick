using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {

	public JoystickInput moveInputs;
	public JoystickInput rotateInputs;
	public Unit controller;

	// Update is called once per frame
	void Update( ) {
		this.controller.moveDirection = new Vector3( this.moveInputs.InputDirection.x, 0, this.moveInputs.InputDirection.y );
		this.controller.aimDirection = new Vector3( this.rotateInputs.InputDirection.x, 0, this.rotateInputs.InputDirection.y );
		this.controller.percentSpeed = this.moveInputs.InputMagnitude;
	}
}
