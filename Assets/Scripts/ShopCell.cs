using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShopCell : MonoBehaviour
{
	public BaseUnit currentUnit;
	public Button unitButton;	

	protected UnitManager unitManager;
	protected ChessBoard board;

	public void AddUnit (UnitManager unitManager, ChessBoard newBoard) {
		board = newBoard;
		this.unitManager = unitManager;
		unitButton.interactable = true;

	}

	public void OnUnitButton() {

		for (int i = 0; i < 10; i++) {
			if (board.benchCells[i,0].currentUnit != null) {
        	} else {
        		currentUnit = unitManager.createUnit(board.benchCells[i, 0]);
        		unitButton.interactable = false;
        		break;
        	}
		}
	}


}
