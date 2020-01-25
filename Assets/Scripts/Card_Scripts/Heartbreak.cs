using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Heartbreak")]
public class Heartbreak : Card
{    
    override public void Activate(BaseUnit unit) {
    	if (unit == null) {
			Debug.LogWarning("Invald Unit Input");
		}
		
		unit.health = unit.health + 2;

		HealthBar hb = unit.GetComponentInChildren<HealthBar>();
        hb.health = unit.health;
        hb.max_health = hb.max_health + 2;
    	
    	this.isUsed = true;
    }
}
