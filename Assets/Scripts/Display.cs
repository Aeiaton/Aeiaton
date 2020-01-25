using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour {
	public Card card;

	public Text card_name;
	public Text card_text;

	public Image artwork;

	public Text cost;

    public void Setup() {
        card_name.text = card.card_name;
        card_text.text = card.text;

        artwork.sprite = card.card_image;

        cost.text = card.cost.ToString();
    }
}
