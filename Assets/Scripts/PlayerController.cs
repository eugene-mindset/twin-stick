using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour, IUnitController {

	public JoystickInput moveInputs;
	public JoystickInput rotateInputs;
	
	public Unit unit;
	public Unit Unit { get => this.unit; set => this.unit = value; }

	// Update is called once per frame
	void Update( ) {
		// Apply inputs to the unit the player is in control of
		this.unit.moveDirection = new Vector3( this.moveInputs.InputDirection.x, 0, this.moveInputs.InputDirection.y );
		this.unit.aimDirection = new Vector3( this.rotateInputs.InputDirection.x, 0, this.rotateInputs.InputDirection.y );
		this.unit.percentSpeed = this.moveInputs.InputMagnitude;
	}
}
