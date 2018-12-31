using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ensure that the AI agents ui is always facing the camera
/// </summary>
public class FaceCamera : MonoBehaviour
{
    public Camera CameraToFace;

    void Start()
    {
        CameraToFace = Camera.main;
    }

    void Update()
    {
        transform.LookAt(transform.position + CameraToFace.transform.rotation * Vector3.forward, CameraToFace.transform.rotation * Vector3.up);
    }
}
