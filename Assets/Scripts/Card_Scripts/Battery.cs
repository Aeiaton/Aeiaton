using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Battery")]
public class Battery : Card
{    
    override public void Activate(BaseUnit unit) {    	
    	Debug.Log("Battery");
    	this.isUsed = true;
    }
}
