using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public string type;
    public string id;
    public string[] connectedTo;

    // Start is called before the first frame update
    void Start()
    {
        foreach (string node in connectedTo) {
            MapHandler mapHandler = GetComponentInParent<MapHandler>();
            mapHandler.AddEdge(id, node);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {

    }

    void OnMouseEnter()
    {
        Debug.Log("###: OnMouseEnter");
        transform.localScale += new Vector3(1f, 1f, 0f);
    }

    void OnMouseExit()
    {
        Debug.Log("###: OnMouseExit");
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
