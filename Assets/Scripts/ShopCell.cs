using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShopCell : MonoBehaviour
{
	public Item currentItem;
	public Button unitButton;	

	public void AddItem (Item item) {
		currentItem = item;
		unitButton.interactable = true;

	}

	public void OnUnitButton() {

		Inventory.instance.Add(currentItem);
		unitButton.interactable = false;
	}


}
