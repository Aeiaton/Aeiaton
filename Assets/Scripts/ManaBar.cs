using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ManaBar : MonoBehaviour
{

    // private Image foreground;
    private RectTransform foregroundTransform;
    private float max_width;
    private float height;

    public float max_mana;
    public float mana = 0;

    // Start is called before the first frame update
    public void Start()
    {

        // foreground = this.GetComponentInChildren(typeof(Image)) as Image;
        // foregroundTransform = this.GetComponentInChildren(typeof(RectTransform)) as RectTransform;
        foregroundTransform = this.transform.Find("Mana Foreground").GetComponent<RectTransform>();

        RectTransform transform = this.GetComponent<RectTransform>();
        max_width = transform.rect.width;
        height = transform.rect.height;
    }

    public void Setup(float h) {

        max_mana = h;
    }

    // Update is called once per frame
    void Update() {
        if (mana >= max_mana) mana = max_mana;
        foregroundTransform.sizeDelta = new Vector2(mana / max_mana * max_width, height);
        
    }
}
