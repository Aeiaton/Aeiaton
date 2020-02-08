using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Node : MonoBehaviour
{

    public enum NodeType {
        Unknown,
        Power,
        Monster1,
        Monster2,
        Something
    };

    private Vector3 scaleStart = new Vector3(1f, 1f, 0f);
    private Vector3 scaleEnd = new Vector3(2f, 2f, 0f);
    private const float swellTime = 0.1f;
    private float elapsed = 0f;
    private bool animating = false;
    private bool enlarging = false;

    public NodeType type = NodeType.Unknown;
    public string id;
    public string[] connectedTo;
    private bool revealed;

    void Start()
    {
        foreach (string node in connectedTo) {
            MapHandler mapHandler = GetComponentInParent<MapHandler>();
            mapHandler.AddEdge(id, node);
        }

        if (this.gameObject.name.Equals("Node_1")) {
            Reveal();
        }
        
    }

    public void Reveal()
    {
        revealed = true;
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Node_" + type.ToString());
    }

    void Update() {
        if (animating && enlarging) {
            elapsed += Time.deltaTime;
            Vector3 scale = Vector3.Lerp(scaleStart, scaleEnd, elapsed / swellTime);
            transform.localScale = scale;
            if (scaleEnd.x - scale.x <= 0.1) {
                animating = false;
                elapsed = 0;
            }
        } else if (animating && !enlarging) {
            elapsed += Time.deltaTime;
            Vector3 scale = Vector3.Lerp(scaleEnd, scaleStart, elapsed / swellTime);
            transform.localScale = scale;
            if (scale.x - scaleStart.x <= 0.1) {
                animating = false;
                elapsed = 0;
            }
        }
    }

    void OnMouseDown()
    {
        if (revealed) {
            MapHandler mapHandler = GetComponentInParent<MapHandler>();
            mapHandler.ActivateNode(id);
        }
    }

    void OnMouseEnter()
    {
        if (revealed) {
            animating = true;
            enlarging = true;
        }
    }

    void OnMouseExit()
    {
        if (revealed) {
            animating = true;
            enlarging = false;
        }
    }
}
