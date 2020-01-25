using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, 
	IPointerEnterHandler, IPointerExitHandler {

	public Transform prevParent = null;
	private int indexNumber;

	public bool hand = true;
	private bool dragging = false;

	public void OnBeginDrag(PointerEventData eventData) {

		// Debug.Log(this.GetComponent<Display>().card.text);
		prevParent = this.transform.parent;
		this.transform.SetParent(this.transform.parent.parent);

		GetComponent<CanvasGroup>().blocksRaycasts = false;
		dragging = true;
	}
	
	public void OnDrag(PointerEventData eventData) {
		
		this.transform.localScale = new Vector3(.25f, .25f, 1f);
		this.transform.position = eventData.position;
	}

	public void OnEndDrag(PointerEventData eventData) {
		this.transform.SetParent (prevParent);
		this.transform.SetSiblingIndex(indexNumber);
		this.transform.localScale = new Vector3(1f, 1f, 1f);

		GetComponent<CanvasGroup>().blocksRaycasts = true;
		if (!hand) {
			this.transform.localScale = new Vector3(0.15f, 0.15f, 1f);
			Destroy(this);
		}

		dragging = true;
	}
	
	public void OnPointerEnter(PointerEventData pointerEventData) {
		

		if (!dragging && !pointerEventData.dragging ) {		
			// Scale Card Up
			this.GetComponent<RectTransform>().sizeDelta = new Vector2(700, 680);
			this.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
			this.transform.position = this.transform.position + new Vector3(0, 50, 0);

			prevParent = this.transform.parent;
			indexNumber = this.transform.GetSiblingIndex();

		}
	}

	public void OnPointerExit(PointerEventData pointerEventData) {
		
		if (!dragging && !pointerEventData.dragging) {

			this.transform.position = this.transform.position - new Vector3(0, 50, 0);
			this.transform.localScale = new Vector3(1f, 1f, 1f);
			this.GetComponent<RectTransform>().sizeDelta = new Vector2(480, 680);
		}

		dragging = false;
	}
	

}