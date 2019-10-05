using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{

  
	public static ShopManager instance;


	void Awake () {
		if (instance != null) {
			Debug.LogWarning("More than one instance of the shop");
		}

		instance = this;
	}
  	
  	public Transform itemsParent;
    public ShopCell[] cells;

	public void Setup(UnitManager unitManager, ChessBoard board) {
        cells = itemsParent.GetComponentsInChildren<ShopCell>();

		// TODO: Randomize Shop based on a selection in a pool of units
		// 		 Make unit construction function based on unit details 
		for (int i = 0; i < 5; i++) {
            cells[i].AddUnit(unitManager, board);
        }


	}





}
