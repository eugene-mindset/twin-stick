using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	private Rigidbody rbody;

	[Header( "Inputs" )]
	public Vector3 moveDirection;
	public Vector3 aimDirection;
	public float percentSpeed;

	[Header( "Move Speed" )]
	public float terminalSpeedWhileMoving;
	public float terminalSpeedWhileAiming;

	[Header( "Rotate Speed" )]
	public float rotateSpeedWhileMoving;
	public float rotateSpeedWhileAiming;

	[Header( "Primary Attack Settings" )]
	public float primaryLifetime;
	public float primaryCooldown;
	public float primaryWait;

	public Vector3 primaryInstatiateOffset;
	public Bullet primaryBulletPrefab;

	public float primarySpeed;
	public float primaryDamage;

	[Header( "Health" )]
	public float maxHealth;
	public float currHealth;

	[Header( "Team Settings" )]
	public byte teamID;

	// Start is called before the first frame update
	void Start( ) {
		// Get rbody
		this.rbody = this.transform.GetComponent<Rigidbody>();
	}

	// FixedUpdate is called once per frame for a consistent duration
	void FixedUpdate( ) {
		// Update the cooldown of primary attack and the state of moving and aiming
		this.primaryWait -= Time.fixedDeltaTime;
		bool aiming = this.aimDirection != Vector3.zero;
		bool moving = this.moveDirection != Vector3.zero;

		// Update traversal and rotation depending on whether unit is aiming or freely moving
		// TODO: Fix quaternion stuff, it doesn't play nice unless I freeze certain axes of rotation
		if ( aiming ) {
			this.rbody.rotation = Quaternion.Lerp( this.rbody.rotation, Quaternion.LookRotation( this.aimDirection ), this.rotateSpeedWhileAiming );
			this.rbody.velocity = this.moveDirection * this.terminalSpeedWhileAiming * this.percentSpeed;

			// Fire primary attack automatically when aiming
			this.FirePrimary();
		} else if ( moving ) {
			this.rbody.rotation = Quaternion.Lerp( this.rbody.rotation, Quaternion.LookRotation( this.moveDirection ), this.rotateSpeedWhileMoving );
			this.rbody.velocity = this.transform.forward * this.terminalSpeedWhileMoving * this.percentSpeed;
		}
	}

	public void FirePrimary( ) {
		// If off cooldown, fire primary
		if ( this.primaryWait <= 0 ) {
			// Set cooldown
			this.primaryWait = this.primaryCooldown;

			// Instatiate new bullet
			Vector3 globalInstatiatePos = this.transform.TransformPoint( this.primaryInstatiateOffset );
			Bullet newBullet = Instantiate( this.primaryBulletPrefab, globalInstatiatePos, this.transform.rotation );

			// Set bullet information
			newBullet.direction = globalInstatiatePos - this.transform.position;
			newBullet.speed = this.primarySpeed;
			newBullet.lifetime = this.primaryLifetime;
			newBullet.teamID = this.teamID;
			newBullet.damage = this.primaryDamage;
		}
	}
}
