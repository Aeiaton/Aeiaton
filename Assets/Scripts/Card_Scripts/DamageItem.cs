using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/DmgItem")]
public class DamageItem : Card
{    
    override public void Activate(BaseUnit unit) {
    	if (unit == null) {
			Debug.LogWarning("Invald Unit Input");
		}
		
		unit.IncrDamage(atkMod);
    	this.isUsed = true;
    }
}
