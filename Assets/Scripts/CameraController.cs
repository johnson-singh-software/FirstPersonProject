using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform cameraFollowTarget;

    private Boolean follow = true;
    public Boolean Follow
    {
        get => follow;
        set => follow = !follow;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void LateUpdate()
    {
        if (follow)
        {
            var tempCamObject = transform;
            tempCamObject.position = cameraFollowTarget.position;
            tempCamObject.rotation = cameraFollowTarget.rotation;
        }
        else
        {
            transform.position = cameraFollowTarget.position;
        }

    }
}
