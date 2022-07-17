using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera)), ExecuteInEditMode]
public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform focusOn;

    private Camera cam;

    protected void Update()
    {
        if (cam == null)
        {
            cam = GetComponent<Camera>();
        }
        
        Bounds bounds = new Bounds(focusOn.GetChild(0).position, Vector3.one);
        foreach (Transform child in focusOn)
        {
            bounds.Encapsulate(child.position);
        }
        transform.position = bounds.center + Vector3.back * 10;
        cam.orthographicSize = Mathf.Max(bounds.size.x, bounds.size.y) / 2f + 0.63f;
    }
}
