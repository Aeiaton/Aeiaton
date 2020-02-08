using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckManager : MonoBehaviour, IPointerClickHandler
{
  
	public static DeckManager instance;


	void Awake () {
		if (instance != null) {
			Debug.LogWarning("More than one instance of Player Deck");
		}

		instance = this;
	}
  	
  	public Transform itemsParent;
  	public Transform handParent;
  	public GameObject cardPanelPrefab;
  	public GameObject cardPrefab;
  	public List<ScriptableObject> cards;
  	public List<ScriptableObject> deck;

	public void Setup() {

		for (int i = 0; i < 40; i++) {
			ScriptableObject randomCard = cards[Random.Range(0, cards.Count)];
			deck.Add(randomCard);

			GameObject card = Instantiate(cardPanelPrefab);

			card.transform.SetParent(itemsParent);
			card.transform.localScale = new Vector3(1, 1, 1);
        }
	}

	    public void OnPointerClick(PointerEventData pointerEventData)
    {
    	if (deck.Count != 0) {
			ScriptableObject cardScript = deck[0];
			deck.RemoveAt(0);
			GameObject drawnCard = this.transform.GetChild(0).gameObject;

			GameObject card = Instantiate(cardPrefab);
			Display display = card.GetComponent<Display>();
			display.card = (Card) Instantiate(cardScript);
			display.Setup();

			card.transform.SetParent(handParent);
			card.transform.localScale = new Vector3(1, 1, 1);

			Destroy(drawnCard);
    	}
    }
}
