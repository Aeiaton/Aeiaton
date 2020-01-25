using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public abstract class Card : ScriptableObject {
   
   public string card_name;
   public string text;

   public Sprite card_image;

   public int cost;

   public bool isUsed;

   public abstract void Activate(BaseUnit unit);

}
