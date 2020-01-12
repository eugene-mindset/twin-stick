using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	private CharacterController charController;

	public Vector3 moveDirection;
	public Vector3 aimDirection;

	public float terminalSpeed;
	public float percentSpeed;
	public float percentSlowdownWhileAiming;

	public float primaryCooldown;
	public float primaryWait;
	public Vector3 primaryInstatiateOffset;
	public Bullet primaryBulletPrefab;
	public float primarySpeed;
	public float primaryLifetime;
	public float primaryDamage;

	public float maxHealth;
	public float currHealth;

	public byte teamID;

	// Start is called before the first frame update
	void Start( ) {
		this.charController = this.transform.GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void FixedUpdate( ) {
		this.primaryWait -= Time.fixedDeltaTime;

		if ( this.aimDirection == Vector3.zero ) {
			this.charController.transform.LookAt( this.charController.transform.position + this.moveDirection );
			this.charController.SimpleMove( this.moveDirection * this.terminalSpeed * this.percentSpeed );
		} else {
			this.charController.transform.LookAt( this.charController.transform.position + this.aimDirection );
			this.charController.SimpleMove( this.moveDirection * this.terminalSpeed * this.percentSpeed * this.percentSlowdownWhileAiming );
			this.FirePrimary();
		}
	}

	public void FirePrimary( ) {
		if ( this.primaryWait <= 0 ) {
			this.primaryWait = this.primaryCooldown;

			Bullet newBullet = Instantiate( this.primaryBulletPrefab, this.transform.TransformPoint( this.primaryInstatiateOffset ), this.transform.rotation );
			newBullet.direction = this.aimDirection;
			newBullet.speed = this.primarySpeed;
			newBullet.lifetime = this.primaryLifetime;
			newBullet.teamID = this.teamID;
			newBullet.damage = this.primaryDamage;
		}
	}
}
