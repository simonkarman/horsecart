public enum HexDirection : int {
    NONE = -1,
    Up = 0,
    RightUp = 1,
    RightDown = 2,
    Down = 3,
    LeftDown = 4,
    LeftUp = 5,
}

public static class HexDirectionExt {
    public static HexDirection Inverse(this HexDirection direction) {
        if (direction == HexDirection.NONE)
            return HexDirection.NONE;
        return (HexDirection)((3 + ((int)direction)) % 6);
    }
}