using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public Vector3 direction;
	public float speed;
	public float lifetime;

	// Start is called before the first frame update
	private void Start( ) {
		Destroy( this.gameObject, this.lifetime );
	}

	// Update is called once per frame
	private void FixedUpdate( ) {
		this.transform.LookAt(this.transform.position + this.direction);
		this.transform.position += this.direction * this.speed * Time.fixedDeltaTime;
	}
}
