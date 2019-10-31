using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	public void OnPointerEnter (PointerEventData eventData) {

	}

	public void OnPointerExit (PointerEventData eventData) {

	}

	public void OnDrop (PointerEventData eventData) {

		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		
		if (d != null) {
			d.prevParent = this.transform;
			d.hand = false;
			Destroy(this);
		}
	}
}
