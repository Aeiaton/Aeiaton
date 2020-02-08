using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropArea : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	public Transform prefab;

	public void OnPointerEnter (PointerEventData eventData) {

	}

	public void OnPointerExit (PointerEventData eventData) {

	}

	public void OnDrop (PointerEventData eventData) {

		// Get information on the draggable and display components
		Draggable drag = eventData.pointerDrag.GetComponent<Draggable>();
		Display display = eventData.pointerDrag.GetComponent<Display>();

		if (drag != null && display != null) {

			// Check if dropped on a unit
			if (this.transform.parent.name == "UnitManager") {
				BaseUnit unit = this.transform.GetComponent<BaseUnit>();
				Card card = display.card;

				if (unit.items.Count < 3) {
					// Remove Card from Hand and Add to Unit Inventory
					Destroy(drag.gameObject);
					unit.items.Add(card);

					// Create new card image above unit
					GameObject cards = unit.transform.Find("Cards").gameObject;

					// Create new Panel Object to display card
					Transform panel;
					panel = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
					panel.SetParent(cards.transform);
					panel.GetChild(0).GetComponent<Image>().sprite = card.card_image;
					panel.localScale = new Vector3(1, 1, 1);

				} else {
					Debug.Log("Unit Has 3 Items Already");
				}
			}
		}
	}
}
