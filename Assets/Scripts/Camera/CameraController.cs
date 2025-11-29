using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 offset;
    public GameObject target;

    // New configurable parameters for orbiting and zooming
    public float rotationSpeed = 5f;
    public float zoomSpeed = 5f;
    public float minDistance = 2f;
    public float maxDistance = 20f;
    public float minPitch = -30f;
    public float maxPitch = 60f;
    public bool invertY = false;

    private float distance;
    private float yaw;
    private float pitch;

    private void Start()
    {
        if (target == null)
        {
            Debug.LogWarning("CameraController: target is not assigned.");
            return;
        }

        // keep the existing offset behavior but also initialize spherical coords
        offset = transform.position - target.transform.position;
        distance = offset.magnitude;

        // initialize yaw/pitch from current transform rotation so camera doesn't jump
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;

        // clamp initial pitch
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
    }
    
    private void LateUpdate()
    {
        if (target == null)
            return;

        // Rotation with right mouse button (hold and drag)
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            yaw += mouseX * rotationSpeed;
            pitch += (invertY ? mouseY : -mouseY) * rotationSpeed;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        }

        // Zoom with scroll wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > Mathf.Epsilon)
        {
            distance = Mathf.Clamp(distance - scroll * zoomSpeed, minDistance, maxDistance);
        }

        // Recalculate position based on spherical coordinates
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 desiredPosition = target.transform.position - (rotation * Vector3.forward * distance);
        transform.position = desiredPosition;
        transform.LookAt(target.transform.position);
    }
}
