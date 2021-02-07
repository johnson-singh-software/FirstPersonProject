using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperCollisionScript : MonoBehaviour
{
    public static int upperLayer = 8; //groundLayer
    public static String otherName;

    private void OnTriggerEnter(Collider other)
    {
        // Ignore trigger events if between this collider and capsule tags, makes switching to Scale much more effective
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("IsChildOf");
            return;
        }
        
        Debug.Log("Upper trigger entry by : " + other.name + " object.");
        var o = other.gameObject;
        upperLayer = o.layer;

        otherName = o.name;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Upper trigger exit, reset to ground");
        upperLayer = 8; //groundLayer
    }
}
