using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class PlayerInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {

	public Image stickPortion;
	public Image gatePortion;
	public Color gizmo;

	public Vector3 inputDirection;

	public void OnPointerDown( PointerEventData eventData ) {
		Debug.Log( "A touch input has been initiated!" );
	}

	public void OnPointerUp( PointerEventData eventData ) {
		Debug.Log( "A touch input has been released!" );
	}

	public void OnBeginDrag( PointerEventData eventData ) {
		return;
	}

	public void OnEndDrag( PointerEventData eventData ) {
		this.stickPortion.transform.position = this.gatePortion.transform.position;
	}

	public void OnDrag( PointerEventData eventData ) {
		this.stickPortion.transform.position = eventData.position;
	}

	private void OnDrawGizmos( ) {
		Gizmos.color = gizmo;
		Gizmos.DrawLine( Camera.main.ScreenToWorldPoint( this.stickPortion.rectTransform.position + Vector3.forward ), Camera.main.ScreenToWorldPoint( this.gatePortion.rectTransform.position + Vector3.forward ) );
	}
}

