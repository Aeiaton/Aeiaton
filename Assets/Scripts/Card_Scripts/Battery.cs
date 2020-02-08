using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Battery")]
public class Battery : Card
{    
    override public void Activate(BaseUnit unit) {    	
    	if (unit == null) {
			Debug.LogWarning("Invald Unit Input");
		}

		unit.mana = unit.mana + 2;

		ManaBar mb = unit.GetComponentInChildren<ManaBar>();
        mb.mana = unit.mana;
    	
    	this.isUsed = true;
    }
}
