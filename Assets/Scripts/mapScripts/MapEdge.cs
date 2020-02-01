using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEdge : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void init(Vector3 from, Vector3 to) {
        float length = Vector3.Distance(from, to);
        float angle = Vector3.Angle(to - from, new Vector3(1f, 0f, 0f));
        float xOffset = length / 2.0f * Mathf.Cos(angle * Mathf.PI / 180f);
        float yOffset = length / 2.0f * Mathf.Sin(angle * Mathf.PI / 180f);
        
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.drawMode = SpriteDrawMode.Sliced;
        spriteRenderer.size = new Vector2(length / 100f, 0.025f);
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        transform.Rotate(Vector3.forward * angle);
        transform.position = new Vector3(from.x + xOffset, from.y + yOffset, 1f);
    }
}
