using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour
{

    public GameObject mapEdgePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddEdge(string from, string to) {
        Node fromNode;
        Node toNode;
        GameObject fromGameObject = null;
        GameObject toGameObject = null;
        for (int i = 0; i < this.gameObject.transform.childCount; i++) {
            GameObject gameObject = this.gameObject.transform.GetChild(i).gameObject;
            Node node = gameObject.GetComponent(typeof(Node)) as Node;
            if (node != null && node.id == from) {
                fromNode = node;
                fromGameObject = gameObject;
            }
            else if (node != null && node.id == to) {
                toNode = node;
                toGameObject = gameObject;
            }
        }
        GameObject mapEdge = Instantiate(mapEdgePrefab, new Vector3() ,Quaternion.identity);
        MapEdge mapEdgeComponent = mapEdge.GetComponent(typeof(MapEdge)) as MapEdge;
        if (fromGameObject != null && toGameObject != null) {
            mapEdgeComponent.init(fromGameObject.transform.position, toGameObject.transform.position);
        }
    }
}
