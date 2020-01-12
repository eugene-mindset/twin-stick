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
		this.hitbox = this.GetComponent<Collider>();
	}

	// Update is called once per frame
	private void FixedUpdate( ) {
		this.transform.LookAt( this.transform.position + this.direction );
		this.transform.position += this.direction * this.speed * Time.fixedDeltaTime;
		this.lifetime -= Time.fixedDeltaTime;

		if ( this.lifetime <= 0 ) {
			GameObject.Destroy( this.gameObject );
		}
	}

	private void OnCollisionEnter( Collision collision ) {
		Debug.Log( "HIT!" );
		Unit otherUnit = collision.gameObject.GetComponent<Unit>();

		if ( otherUnit && otherUnit.teamID != this.teamID ) {
			otherUnit.currHealth -= this.teamID;
			GameObject.Destroy( this.gameObject );
		}
	}
}
