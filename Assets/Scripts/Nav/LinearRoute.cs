using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class LinearRoute : MonoBehaviour
{
    //public Transform destination;
    private NavMeshAgent navMeshAgent;

    public bool waitAtDestination, onRoute, waiting, flipDirection;
    public float waitingTime = 0f, flipDirectionProbability = 0.5f, waitProbability = 0.7f, maxWaitTime = 3f;

    public List<Node> destinationNodes;
    private int currentNode;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (navMeshAgent == null)
        {
            Debug.Log("navMeshAgent has not been assigned object");
        }
        else
        {
            if (destinationNodes != null && destinationNodes.Count >= 2)
            {
                currentNode = Random.Range(0, destinationNodes.Count);
                setDestination();
            }
            else
            {
                Debug.Log("Less than 2 Nodes available for routing");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (onRoute && navMeshAgent.remainingDistance <= 1f) //in final stage of route to node
        {
            onRoute = false;
            
            waitAtDestination = (Random.value <= waitProbability);

            if (waitAtDestination) //if probability has set agent to wait at node
            {
                waiting = true;
                waitingTime = 0f;
            }
            else //continue new route
            {
                calculateNextNode();
                setDestination();
            }
        }
        
        if (waiting)
        {
            waitingTime += Time.deltaTime; //1 second
            if (waitingTime >= maxWaitTime)
            {
                waiting = false;

                calculateNextNode();
                setDestination();
            }
        }
    }

    private void setDestination()
    {
        if (destinationNodes != null)
        {
            Vector3 targetDestination = destinationNodes[currentNode].transform.position;
            navMeshAgent.SetDestination(targetDestination);
            onRoute = true;
        }
        else
        {
            Debug.Log("destinationNodes has not been assigned a list of Nodes");
        }
    }

    private void calculateNextNode()
    {
        flipDirection = (Random.value <= flipDirectionProbability);

        if (flipDirection)
        {
            currentNode++;
            if (currentNode >= destinationNodes.Count)
            {
                currentNode = 0; //flip direction/loop around all the way back to 0
            }
        }
        else
        {
            currentNode--;
            if (currentNode <= 0)
            {
                currentNode = destinationNodes.Count - 1; //flip direction/loop around all the way back to end
            }
        }

    }
}