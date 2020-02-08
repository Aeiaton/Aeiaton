using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapEdge : MonoBehaviour
{
    private bool activated = false;
    private Vector3 start;
    private Vector3 end;
    private Color edgeColor;
    private SpriteRenderer spriteRenderer;

    void Start() {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        edgeColor = spriteRenderer.color;
    }

    void Update() {
        if (activated) {
            spriteRenderer.color = new Color(26f / 255f, 110f / 255f, 134f/ 255f, (float) (0.25f * Math.Sin(4 * Time.time) + 0.75f));
        }
    }

    public void activate() {
        activated = true;
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Color newColor = spriteRenderer.color;
        newColor.a = 1f;
        spriteRenderer.color = newColor;
    }

    public void draw(Vector3 from, Vector3 to) {
        start = from;
        end = to;
        float length = Vector3.Distance(from, to);
        float angle = Vector3.Angle(to - from, new Vector3(1f, 0f, 0f));
        float xOffset = length / 2.0f * Mathf.Cos(angle * Mathf.PI / 180f);
        float yOffset = length / 2.0f * Mathf.Sin(angle * Mathf.PI / 180f);
        
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.drawMode = SpriteDrawMode.Sliced;
        spriteRenderer.size = new Vector2(length / 100f, 0.015f);
        spriteRenderer.color = new Color(26f / 255f, 110f / 255f, 134f/ 255f, 0.2f);
        transform.Rotate(Vector3.forward * angle);
        transform.position = new Vector3(from.x + xOffset, from.y + yOffset, 1f);
    }
}
