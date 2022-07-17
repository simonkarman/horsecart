using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouController : MonoBehaviour
{
    [SerializeField]
    private World world;
    
    protected void Update()
    {
        AxialCoordinate direction = AxialCoordinate.Zero;
        if (Input.GetKeyUp(KeyCode.Q)) {
            direction = AxialCoordinate.LeftUp;
        } else if (Input.GetKeyUp(KeyCode.W)) {
            direction = AxialCoordinate.Up;
        } else if (Input.GetKeyUp(KeyCode.E)) {
            direction = AxialCoordinate.RightUp;
        } else if (Input.GetKeyUp(KeyCode.D)) {
            direction = AxialCoordinate.RightDown;
        } else if (Input.GetKeyUp(KeyCode.S)) {
            direction = AxialCoordinate.Down;
        } else if (Input.GetKeyUp(KeyCode.A))
        {
            direction = AxialCoordinate.LeftDown;
        }
        AxialCoordinate location = AxialCoordinate.FromPixel(transform.position, 0.63f);
        if (CanMoveTo(location + direction))
        {
            SetLocation(location + direction);
        }
    }

    private bool CanMoveTo(AxialCoordinate location)
    {
        return world.HasTileAt(location);
    }

    public void SetLocation(AxialCoordinate location)
    {
        transform.position = location.ToPixel(0.63f);
    }
}
