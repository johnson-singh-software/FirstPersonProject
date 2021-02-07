using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private readonly Vector3 nodeWireframeSize = new Vector3(2f, 2f, 2f);

    void OnDrawGizmos()
    {
        Vector3 pos = transform.position;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(pos,  nodeWireframeSize);
    }
    
}
