using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{

    // private Image foreground;
    private RectTransform foregroundTransform;
    private float max_width;
    private float height;

    public float max_health;
    public float health;

    // Start is called before the first frame update
    void Start()
    {

        // foreground = this.GetComponentInChildren(typeof(Image)) as Image;
        // foregroundTransform = this.GetComponentInChildren(typeof(RectTransform)) as RectTransform;
        foregroundTransform = this.transform.Find("Foreground").GetComponent<RectTransform>();

        RectTransform transform = this.GetComponent<RectTransform>();
        max_width = transform.rect.width;
        height = transform.rect.height;

        BaseUnit b = this.GetComponentInParent<BaseUnit>();
        health = b.health;
        max_health = b.health;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) health = 0;
        foregroundTransform.sizeDelta = new Vector2(health / max_health * max_width, height);
        float offset = (max_width - (health / max_health * max_width)) / 2;
        foregroundTransform.localPosition = new Vector2(-offset, 0);
    }
}
