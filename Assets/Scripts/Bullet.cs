using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public Vector3 direction;
	public float speed;
	public float lifetime;
	public float damage;
	public Collider hitbox;
	public byte teamID;

	// Start is called before the first frame update
	private void Start( ) {
		// Get collider representing hitbox of bullet, have bullet look in proper direction
		this.hitbox = this.GetComponent<Collider>();
		this.transform.LookAt( this.transform.position + this.direction );
	}

	// FixedUpdate is called once per frame for a consistent duration
	private void FixedUpdate( ) {
		// Update direction, position, and lifetime
		this.transform.LookAt( this.transform.position + this.direction );
		this.transform.position += this.direction * this.speed * Time.fixedDeltaTime;
		this.lifetime -= Time.fixedDeltaTime;

		// when lifetime reaches 0, destroy bullet
		if ( this.lifetime <= 0 ) {
			GameObject.Destroy( this.gameObject );
		}
	}

	//
	private void OnTriggerEnter( Collider other ) {
		// see if bullet collides with a unit, and deal damage to unit if it was on opposing team
		Unit otherUnit = other.gameObject.GetComponent<Unit>();

		if (otherUnit && otherUnit.teamID != this.teamID) {
			otherUnit.currHealth -= this.damage;
			GameObject.Destroy( this.gameObject );
		}
	}
}
