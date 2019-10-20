using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,
 IPointerEnterHandler, IPointerExitHandler {

	public Transform prevParent = null;
	private int indexNumber;
	private bool dragging = false;

	public void OnBeginDrag(PointerEventData eventData) {
		Debug.Log("Begin");

		prevParent = this.transform.parent;
		indexNumber = this.transform.GetSiblingIndex();
		dragging = true;

		this.transform.SetParent (this.transform.parent.parent);
	}
	
	public void OnDrag(PointerEventData eventData) {
		this.transform.localScale = new Vector3(.25f, .25f, 1f);
		this.transform.position = eventData.position;
	}

	public void OnEndDrag(PointerEventData eventData) {
		Debug.Log("End");

		this.transform.SetParent (prevParent);
		this.transform.SetSiblingIndex(indexNumber);
		this.transform.localScale = new Vector3(1f, 1f, 1f);

		dragging = true;
	}

	public void OnPointerEnter(PointerEventData pointerEventData) {
		Debug.Log("Enter");

		if (!dragging) {		
			this.transform.localScale = new Vector3(2f, 2f, 1f);
			this.transform.position = this.transform.position + new Vector3(0, 50, 0);
		}
	}

	public void OnPointerExit(PointerEventData pointerEventData) {
		Debug.Log ("Exit");
		if (!dragging) {
			this.transform.position = this.transform.position - new Vector3(0, 50, 0);
			this.transform.localScale = new Vector3(1f, 1f, 1f);
		}

		dragging = false;
	}

}
