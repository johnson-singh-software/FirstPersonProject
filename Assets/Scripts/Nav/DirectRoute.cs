using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DirectRoute : MonoBehaviour
{

    [SerializeField] private Transform destination;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (navMeshAgent == null)
        {
            Debug.Log("navMeshAgent has not been assigned object");
        }
        else
        {
            setDestination();
        }
    }

    private void setDestination()
    {
        if (destination != null)
        {
            Vector3 targetDestination = destination.transform.position;
            navMeshAgent.SetDestination(targetDestination);
        }
    }
}
