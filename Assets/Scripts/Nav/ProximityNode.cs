using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ProximityNode : Node
{
    public float proximityRadius = 50f;
    private List<ProximityNode> nodes = new List<ProximityNode>();

    private void Start()
    {
        //SphereOverlap later
        GameObject[] allNodes = GameObject.FindGameObjectsWithTag("Node");

        foreach (var n in allNodes)
        {
            ProximityNode nextNode = n.GetComponent<ProximityNode>();

            if (nextNode != null)
            {
                if (Vector3.Distance(this.transform.position, nextNode.transform.position) <= proximityRadius && nextNode != this)
                {
                    nodes.Add(nextNode);
                }
            }
        }
    }
    
//    private readonly Vector3 nodeWireframeSize = new Vector3(2f, 2f, 2f);
//    void OnDrawGizmos()
//    {
//        Vector3 pos = transform.position;
//
//        Gizmos.color = Color.cyan;
//        Gizmos.DrawWireSphere(pos,  2f);
//        
//        Gizmos.color = Color.green;
//        Gizmos.DrawWireSphere(pos,  proximityRadius);
//    }

    public ProximityNode calculateNextNode(ProximityNode previousNode)
    {
        if (nodes.Count == 0)
        {
            Debug.Log("No nodes within proximity radius");
            return null;
        }
        else if (nodes.Count == 1 && nodes.Contains(previousNode))
        {
            return previousNode;
        }
        else
        {
            ProximityNode nextNode;
            do
            {
                int n = UnityEngine.Random.Range(0, nodes.Count);
                nextNode = nodes[n];
            } while (nextNode == previousNode);

            return nextNode;
        }
    }
}