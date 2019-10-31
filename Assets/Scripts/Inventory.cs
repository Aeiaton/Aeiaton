using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public static Inventory instance;

	void Awake() {
		if (instance != null) {
			Debug.LogWarning("More than one instance");
			return;
		}
		instance = this;
	}

	public int space = 10;

    public List<Item> items = new List<Item>();

    public void Add (Item item) {
    	if (items.Count >= space) {
    		Debug.Log("Out of space");
    		return;
    	}

    	items.Add(item);
    }

    public void Remove (Item item) {
    	items.Remove(item);
    }
}
