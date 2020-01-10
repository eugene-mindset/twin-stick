using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class JoystickInput : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {

	public Image stickPortion;
	public Image gatePortion;
	public Color gizmoColor;
	public float maxMagnitude;

	private Vector2 inputDir;
	private float inputMag;

	public Vector2 InputDirection { get => this.inputDir; }
	public float InputMagnitude { get => this.inputMag; }
	public void OnBeginDrag( PointerEventData eventData ) {
		this.OnDrag( eventData );
	}

	public void OnEndDrag( PointerEventData eventData ) {
		this.stickPortion.transform.position = this.gatePortion.transform.position;
		this.inputDir = Vector2.zero;
		this.inputMag = 0;
	}

	public void OnDrag( PointerEventData eventData ) {
		Vector2 basePosition = new Vector2( this.gatePortion.rectTransform.position.x, this.gatePortion.rectTransform.position.y );
		Vector2 dragVector = eventData.position - basePosition;

		float magnitude = dragVector.magnitude;
		Vector2 direction = dragVector.normalized;

		magnitude = magnitude > this.maxMagnitude ? this.maxMagnitude : magnitude;

		this.stickPortion.rectTransform.anchoredPosition = direction * magnitude;

		this.inputDir = direction;
		this.inputMag = magnitude / this.maxMagnitude;
	}

	private void OnDrawGizmos( ) {
		float zOff = Camera.main.nearClipPlane;
		Vector3 stickPos = new Vector3( this.stickPortion.rectTransform.position.x, this.stickPortion.rectTransform.position.y, zOff );
		Vector3 gatePos = new Vector3( this.gatePortion.rectTransform.position.x, this.gatePortion.rectTransform.position.y, zOff );

		Gizmos.color = this.gizmoColor;
		Gizmos.DrawLine( Camera.main.ScreenToWorldPoint( stickPos ), Camera.main.ScreenToWorldPoint( gatePos ) );
	}
}

