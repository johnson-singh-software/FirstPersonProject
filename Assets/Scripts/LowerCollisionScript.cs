using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerCollisionScript : MonoBehaviour
{
    public static int lowerLayer = 8; //ground

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Lower trigger entry by : " + other.name + " object.");
        lowerLayer = other.gameObject.layer;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Lower trigger exit, reset to ground.");
        lowerLayer = 8; //groundLayer
    }
}