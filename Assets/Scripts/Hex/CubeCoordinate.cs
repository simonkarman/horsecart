using System;
using UnityEngine;

[System.Serializable]
public struct CubeCoordinate {
    [SerializeField]
    private float x;
    [SerializeField]
    private float y;
    [SerializeField]
    private float z;

    public CubeCoordinate(float x, float y, float z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public float this[int index] {
        get {
            switch (index) {
            case 0:
                return x;
            case 1:
                return y;
            case 2:
                return z;
            default:
                throw new IndexOutOfRangeException();
            }
        }
    }

    public static CubeCoordinate operator +(CubeCoordinate self, CubeCoordinate other) {
        return new CubeCoordinate(self.x + other.x, self.y + other.y, self.z + other.z);
    }

    public static CubeCoordinate operator +(CubeCoordinate self, HexDirection direction) {
        if (direction == HexDirection.NONE)
            return self;
        return self + Directions[(int)direction];
    }

    public static CubeCoordinate operator -(CubeCoordinate self, CubeCoordinate other) {
        return new CubeCoordinate(self.x - other.x, self.y - other.y, self.z - other.z);
    }

    public static CubeCoordinate operator -(CubeCoordinate self, HexDirection direction) {
        if (direction == HexDirection.NONE)
            return self;
        return self - Directions[(int)direction];
    }

    public static bool operator ==(CubeCoordinate self, CubeCoordinate other) {
        return (self.x == other.x && self.y == other.y && self.z == other.z);
    }

    public static bool operator !=(CubeCoordinate self, CubeCoordinate other) {
        return !(self == other);
    }

    public static CubeCoordinate operator *(CubeCoordinate self, float s) {
        return new CubeCoordinate(self.x * s, self.y * s, self.z * s);
    }

    public CubeCoordinate Rounded() {
        float roundedX = Mathf.Round(x);
        float roundedY = Mathf.Round(y);
        float roundedZ = Mathf.Round(z);

        float xDiff = Mathf.Abs(roundedX - x);
        float yDiff = Mathf.Abs(roundedY - y);
        float zDiff = Mathf.Abs(roundedZ - z);

        if ((xDiff > yDiff) && (xDiff > zDiff)) {
            roundedX = -roundedY - roundedZ;
        }
        else if (yDiff > zDiff) {
            roundedY = -roundedX - roundedZ;
        }
        else {
            roundedZ = -roundedX - roundedY;
        }

        return new CubeCoordinate(roundedX, roundedY, roundedZ);
    }

    public AxialCoordinate ToAxial() {
        return new AxialCoordinate(x, z);
    }

    public static readonly CubeCoordinate Zero = new CubeCoordinate(0, 0, 0);
    public static readonly CubeCoordinate Up = new CubeCoordinate(0, -1, +1);
    public static readonly CubeCoordinate RightUp = new CubeCoordinate(+1, -1, 0);
    public static readonly CubeCoordinate RightDown = new CubeCoordinate(+1, 0, -1);
    public static readonly CubeCoordinate Down = new CubeCoordinate(0, +1, -1);
    public static readonly CubeCoordinate LeftDown = new CubeCoordinate(-1, +1, 0);
    public static readonly CubeCoordinate LeftUp = new CubeCoordinate(-1, 0, +1);
    public static readonly CubeCoordinate[] Directions = new CubeCoordinate[] { Up, RightUp, RightDown, Down, LeftDown, LeftUp };

    public override string ToString() {
        return "Cube(" + x + ", " + y + ", " + z + ")";
    }

    public override bool Equals(object obj) {
        if (obj == null || GetType() != obj.GetType())
            return false;

        CubeCoordinate other = (CubeCoordinate)obj;
        return (x == other.x) && (y == other.y) && (z == other.z);
    }

    public override int GetHashCode() {
        int hash = 17;
        hash = hash * 23 + x.GetHashCode();
        hash = hash * 23 + y.GetHashCode();
        hash = hash * 23 + z.GetHashCode();
        return hash;
    }

    public float GetLength() {
        return (Mathf.Abs(x) + Mathf.Abs(y) + Mathf.Abs(z)) / 2;
    }

    public float Distance(CubeCoordinate other) {
        return (Mathf.Abs(x - other.x) + Mathf.Abs(y - other.y) + Mathf.Abs(z - other.z)) / 2;
    }
}

