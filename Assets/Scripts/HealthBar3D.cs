using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO: Display number of hitpoints left?
public class HealthBar3D : MonoBehaviour {

	public Unit unit;
	public Image healthPoriton;
	public Vector3 offset;
	public Vector3 size;
	public float lerpSpeed;

	// Start is called before the first frame update
	void Start( ) {
		this.transform.localScale = this.size;
	}

	// LateUpdate is called once per frame, after game logic is updated
	void LateUpdate( ) {
		// If the current scale of bar is not the set scale update it
		if ( this.transform.localScale != this.size ) {
			this.transform.localScale = this.size;
		}

		// Update the current health portion to reflect the current amount of health
		this.healthPoriton.rectTransform.sizeDelta = new Vector2(
			Mathf.Lerp( this.healthPoriton.rectTransform.sizeDelta.x, this.unit.currHealth / this.unit.maxHealth, this.lerpSpeed ),
			1.0f );

		// Have the UI hover over unit and make it face towards the screen
		this.transform.position = this.unit.transform.position + this.offset;
		this.transform.right = -Camera.main.transform.right;
		this.transform.up = -Camera.main.transform.up;
		this.transform.forward = -Camera.main.transform.forward;
	}
}
