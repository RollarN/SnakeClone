using UnityEngine;

public static class DirectionUtility
{
    /// <summary>Gets the directions corresponding vector
    /// </summary>
    public static Vector2 GetDirectionVector(this Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return Vector2.up;
            case Direction.Right:
                return Vector2.right;
            case Direction.Left:
                return Vector2.left;
            case Direction.Down:
                return Vector2.down;
            default:
                throw new System.ArgumentException
                    ("Direction has no respective vector - Add switch case to enum value");
        }
    }
    /// <summary>Checks if the input direction is perpendicular to this direction
    /// </summary>
    public static bool IsPerpendicularTo(this Direction thisDirection, Direction newDirection)
    {
        if ((thisDirection == Direction.Up) || (thisDirection == Direction.Down))
            return ((newDirection == Direction.Left) || (newDirection == Direction.Right));
        if ((thisDirection == Direction.Right) || (thisDirection == Direction.Left))
            return ((newDirection == Direction.Up) || (newDirection == Direction.Down));
        throw new System.ArgumentException("Invalid Direction");
    }
}

public enum Direction
{
    Up, Right, Down, Left
}
