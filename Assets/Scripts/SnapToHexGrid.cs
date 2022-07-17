using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SnapToHexGrid : MonoBehaviour {
  protected void Update()
  {
    transform.localPosition = AxialCoordinate.FromPixel(transform.localPosition, 0.63f).Rounded().ToPixel(0.63f);
  }
}
