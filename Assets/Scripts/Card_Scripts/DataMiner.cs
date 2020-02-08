using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/DataMiner")]
public class DataMiner : Card
{    
    override public void Activate(BaseUnit unit) {    	
    	Debug.Log("DataMiner");
    	this.isUsed = true;
    }
}

