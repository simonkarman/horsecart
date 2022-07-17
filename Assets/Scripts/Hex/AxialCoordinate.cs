using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AxialCoordinate {
    [SerializeField]
    private float q;
    [SerializeField]
    private float r;

    public AxialCoordinate(float q, float r) {
        this.q = q;
        this.r = r;
    }

    public static AxialCoordinate operator +(AxialCoordinate self, AxialCoordinate other) {
        return new AxialCoordinate(self.q + other.q, self.r + other.r);
    }

    public static AxialCoordinate operator +(AxialCoordinate self, HexDirection direction) {
        if (direction == HexDirection.NONE)
            return self;
        return self + Directions[(int)direction];
    }

    public static AxialCoordinate operator -(AxialCoordinate self, AxialCoordinate other) {
        return new AxialCoordinate(self.q - other.q, self.r - other.r);
    }

    public static AxialCoordinate operator -(AxialCoordinate self, HexDirection direction) {
        if (direction == HexDirection.NONE)
            return self;
        return self - Directions[(int)direction];
    }

    public static bool operator ==(AxialCoordinate self, AxialCoordinate other) {
        return Mathf.Approximately(self.q, other.q) && Mathf.Approximately(self.r, other.r);
    }

    public static bool operator !=(AxialCoordinate self, AxialCoordinate other) {
        return !(self == other);
    }

    public static AxialCoordinate operator *(AxialCoordinate self, float s) {
        return new AxialCoordinate(self.q * s, self.r * s);
    }

    public AxialCoordinate Rounded() {
        return ToCube().Rounded().ToAxial();
    }

    public CubeCoordinate ToCube() {
        return new CubeCoordinate(q, -q - r, r);
    }

    public Vector2 ToPixel(float size) {
        return new Vector2(
            size * 3f / 2f * q,
            size * Mathf.Sqrt(3) * (r + q / 2)
        );
    }

    public static AxialCoordinate FromPixel(Vector2 pixel, float size) {
        return new AxialCoordinate(
            pixel.x * 2f / 3f / size,
            (-pixel.x / 3 + Mathf.Sqrt(3) / 3 * pixel.y) / size
        );
    }

    public static readonly AxialCoordinate Zero = new AxialCoordinate(0, 0);
    public static readonly AxialCoordinate Up = new AxialCoordinate(0, +1);
    public static readonly AxialCoordinate RightUp = new AxialCoordinate(+1, 0);
    public static readonly AxialCoordinate RightDown = new AxialCoordinate(+1, -1);
    public static readonly AxialCoordinate Down = new AxialCoordinate(0, -1);
    public static readonly AxialCoordinate LeftDown = new AxialCoordinate(-1, 0);
    public static readonly AxialCoordinate LeftUp = new AxialCoordinate(-1, +1);
    public static readonly AxialCoordinate[] Directions = new AxialCoordinate[] { Up, RightUp, RightDown, Down, LeftDown, LeftUp };

    public override string ToString() {
        return "Axial(" + q + ", " + r + ")";
    }

    public override bool Equals(object obj) {
        if (obj == null || GetType() != obj.GetType())
            return false;

        AxialCoordinate other = (AxialCoordinate)obj;
        return Mathf.Approximately(q, other.q) && Mathf.Approximately(r, other.r);
    }

    public override int GetHashCode() {
        int hash = 17;
        hash = hash * 23 + q.GetHashCode();
        hash = hash * 23 + r.GetHashCode();
        return hash;
    }

    public float GetLength() {
        return ToCube().GetLength();
    }

    public float Distance(AxialCoordinate other) {
        return ToCube().Distance(other.ToCube());
    }

    public static List<AxialCoordinate> Circle(AxialCoordinate center, int radius) {
        List<AxialCoordinate> coords = new List<AxialCoordinate>();
        center = center.Rounded();
        for (int q = (int)(center.q - radius + 1); q < center.q + radius; q++) {
            for (int r = (int)(center.r - radius + 1); r < center.r + radius; r++) {
                AxialCoordinate coord = new AxialCoordinate(q, r);
                if (coord.Distance(center) >= radius) {
                    continue;
                }
                coords.Add(coord);
            }
        }
        return coords;
    }

    public static List<AxialCoordinate> Rectangle(AxialCoordinate center, HexDirection direction, int halfLength, int halfThickness, bool extra) {
        AxialCoordinate primary = Directions[(int)direction];
        AxialCoordinate secondary = Directions[((int)direction + 1) % 6];
        AxialCoordinate tertiary = Directions[((int)direction + 2) % 6];

        List<AxialCoordinate> coords = new List<AxialCoordinate>();
        for (int secondaryI = -halfThickness + 1; secondaryI < halfThickness; secondaryI++) {
            for (int primaryI = -halfLength + 1; primaryI < halfLength; primaryI++) {
                AxialCoordinate coord = center
                    + primary * primaryI
                    + secondary * Mathf.FloorToInt((secondaryI + 1) / 2f)
                    + tertiary * Mathf.FloorToInt(secondaryI / 2f);
                if (extra && (primaryI == -halfLength + 1) && (secondaryI % 2 != 0)) {
                    AxialCoordinate extraCoord = coord - primary;
                    coords.Add(extraCoord);
                }
                coords.Add(coord);
            }
        }
        return coords;
    }
}