using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/MorningBrew")]
public class MorningBrew : Card
{    
    override public void Activate(BaseUnit unit) {    	
    	Debug.Log("MorningBrew");
    	this.isUsed = true;
    }
}


