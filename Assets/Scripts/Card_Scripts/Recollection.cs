using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/HellishRecollection")]
public class HellishRecollection : Card
{    
    override public void Activate(BaseUnit unit) {    	
    	Debug.Log("Hellish Recollection");
    	this.isUsed = true;
    }
}

