using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProximityRoute : MonoBehaviour
{
    //public Transform destination;
    private NavMeshAgent navMeshAgent;
    private ProximityNode currentNode;
    private ProximityNode previousNode;

    public bool waitAtDestination, onRoute, waiting, flipDirection;
    public float waitingTime = 0f, flipDirectionProbability = 0.5f, waitProbability = 0.7f, maxWaitTime = 3f;
    
    private int newNode, nodesVisited;


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (navMeshAgent == null)
        {
            Debug.Log("navMeshAgent has not been assigned object");
        }
        else
        {
            if (currentNode == null)
            {
                GameObject[] allNodes = GameObject.FindGameObjectsWithTag("Node");

                if (allNodes.Length > 0)
                {
                    while (currentNode == null)
                    {
                        int random = Random.Range(0, allNodes.Length);
                        ProximityNode startNode = allNodes[random].GetComponent<ProximityNode>();

                        if (startNode != null)
                        {
                            currentNode = startNode;
                        }
                    }
                }
            }
        }

        setDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (onRoute && navMeshAgent.remainingDistance <= 1f) //in final stage of route to node
        {
            onRoute = false;
            nodesVisited++;

            waitAtDestination = (Random.value <= waitProbability);

            if (waitAtDestination) //if probability has set agent to wait at node
            {
                waiting = true;
                waitingTime = 0f;
            }
            else //continue new route
            {
                //calculateNextNode();
                setDestination();
            }
        }

        if (waiting)
        {
            waitingTime += Time.deltaTime; //1 second
            if (waitingTime >= maxWaitTime)
            {
                waiting = false;

                //calculateNextNode();
                setDestination();
            }
        }
    }

    private void setDestination()
    {
        if (nodesVisited > 0)
        {
            ProximityNode nextNode = currentNode.calculateNextNode(previousNode);
            previousNode = currentNode;
            currentNode = nextNode;
        }

        Vector3 targetDestination = currentNode.transform.position;
        navMeshAgent.SetDestination(targetDestination);
        onRoute = true;
    }
}