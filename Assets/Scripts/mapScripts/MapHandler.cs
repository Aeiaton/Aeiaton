using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour
{

    public GameObject mapEdgePrefab;
    private Dictionary<string, List<GameObject>> edgeObjectMap;
    private Dictionary<string, List<GameObject>> edgeNodeMap;

    void Awake()
    {
        edgeObjectMap = new Dictionary<string, List<GameObject>>();
        edgeNodeMap = new Dictionary<string, List<GameObject>>();
    }

    void Start() {

        for (int i = 0; i < this.gameObject.transform.childCount; i++) {
            GameObject gameObject = this.gameObject.transform.GetChild(i).gameObject;
            Node node = gameObject.GetComponent(typeof(Node)) as Node;
            if (node != null) {
                node.init();
            }
        }

        // Check the node that we are returning from, and mark it as completed
        if (GameData.selectedNode != null) {
            CompleteNode(GameData.selectedNode);
            GameData.selectedNode = null;
        }

        foreach (string node in GameData.completedNodes) {
            CompleteNode(node);
        }

        
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
            
            List<GameObject> connectedEdges = null;
            List<GameObject> connectedNodes = null;

            if (edgeObjectMap.TryGetValue(from, out connectedEdges)) {}
            else { connectedEdges = new List<GameObject>(); }

            if (edgeNodeMap.TryGetValue(from, out connectedNodes)) {}
            else { connectedNodes = new List<GameObject>(); }

            connectedEdges.Add(mapEdge);
            connectedNodes.Add(toGameObject);

            edgeObjectMap[from] = connectedEdges;
            edgeNodeMap[from] = connectedNodes;

            mapEdgeComponent.draw(fromGameObject.transform.position, toGameObject.transform.position);
        }
    }

    public void CompleteNode(string node) {

        if (!GameData.completedNodes.Contains(node)) {
            GameData.completedNodes.Add(node);
        }

        if (edgeObjectMap.ContainsKey(node)) {
            List<GameObject> edges = edgeObjectMap[node];
            foreach (GameObject edge in edges) {
                MapEdge mapEdgeComponent = edge.GetComponent(typeof(MapEdge)) as MapEdge;
                mapEdgeComponent.activate();
            }
        }

        if (edgeNodeMap.ContainsKey(node)) {    
            List<GameObject> nodes = edgeNodeMap[node];
            foreach (GameObject nodeObject in nodes) {
                Node nodeComponent = nodeObject.GetComponent(typeof(Node)) as Node;
                nodeComponent.Reveal();
                if (!GameData.revealedNodes.Contains(nodeComponent.id)) {
                    GameData.revealedNodes.Add(nodeComponent.id);
                }
            }
        }
    }

}
