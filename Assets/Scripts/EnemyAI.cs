using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IUnitController {

	// Start is called before the first frame update
	public static GameController gameControl;

	private Unit unit;
	public Unit Unit { get => this.unit; set => this.unit = value; }

	public Unit closestEnemy;

	// Update is called once per frame
	private void FixedUpdate( ) {
		if (this.unit.currHealth <= 0) {
			return;
		}

		this.closestEnemy = EnemyAI.gameControl.GetPlayer();
		float range = this.unit.primaryLifetime * this.unit.primarySpeed;
		float distance = ( this.transform.position - this.closestEnemy.transform.position ).magnitude;
		Vector3 direction = ( this.transform.position - this.closestEnemy.transform.position ).normalized;

		if ( range <= distance ) {
			this.unit.moveDirection = -direction;
			this.unit.percentSpeed = 1;
		} else {
			this.unit.moveDirection = this.closestEnemy.moveDirection;
			this.unit.aimDirection = -direction;
			this.unit.percentSpeed = 1;
			this.unit.FirePrimary();
		}
	}
}
