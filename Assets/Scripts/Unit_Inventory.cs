using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Inventory : MonoBehaviour
{

	public void Update () {
		foreach(Transform child in this.transform) {
        	Card card = child.GetComponent<Item>().card;

        	if (card.isUsed == false) {
        		
	       		card.Activate(this.transform.parent.GetComponent<BaseUnit>());
        	}
    	}
	}

}
