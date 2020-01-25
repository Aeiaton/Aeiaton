﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Sword")]
public class Sword : Card
{    
    override public void Activate(BaseUnit unit) {
    	if (unit == null) {
			Debug.LogWarning("Invald Unit Input");
		}
		
		unit.IncrDamage(1);
    	this.isUsed = true;
    }
}
