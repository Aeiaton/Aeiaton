using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{

  
	public static HandManager instance;


	void Awake () {
		if (instance != null) {
			Debug.LogWarning("More than one instance of Player Hand");
		}

		instance = this;
	}
  	
  	public Transform itemsParent;
  	public GameObject cardPrefab;
  	public List<ScriptableObject> deck;

	public void Setup() {

		for (int i = 0; i < 5; i++) {
			ScriptableObject randomCard = deck[Random.Range(0, deck.Count)];

			GameObject card = Instantiate(cardPrefab);
			Display display = card.GetComponent<Display>();
			display.card = (Card) Instantiate(randomCard);
			display.Setup();

			card.transform.SetParent(itemsParent);
			card.transform.localScale = new Vector3(1, 1, 1);
        }


	}





}
