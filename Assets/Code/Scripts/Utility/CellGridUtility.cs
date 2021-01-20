using GridCell;
using UnityEngine;

/// <summary>Static class, has access to the grid methods to find cells
/// </summary>
public static class CellGridUtility
{

    private static Cell[,] grid;
    public static Cell[,] Grid
    {
        set => grid = value;
    }

    /// <summary>Finds target cell based on Vector position. Clamps to the grid's size (edge exclusive).
    /// </summary>
    public static Cell VectorToCell(this Vector2 targetPosition)
    {
        int xPosition = (int)Mathf.Clamp((int)targetPosition.x, 1, grid.GetLength(0));
        int yPosition = (int)Mathf.Clamp((int)targetPosition.y, 1, grid.GetLength(1));
        return grid[xPosition, yPosition];
    }
    /// <summary>Finds target cell based on Vector position. Clamps to the grid's size (edge exclusive). 
    /// </summary>
    public static Cell VectorToCell(this Vector3 targetPosition)
    {
        Vector2 TargetPosition2d = targetPosition;
        return TargetPosition2d.VectorToCell();
    }

    /// <summary>Finds a random empty cell inside the grid, given a set amount of attempts. EmptyCell is null if cell isn't found
    /// </summary>
    public static (bool FoundEmptyCell, Cell EmptyCell) FindRandomEmptyCell(int attempts = 40)
    {
        var targetCell = GetRandomCell(2);
        while (targetCell.Occupant != null && attempts > 0)
        {
            attempts--;
            targetCell = GetRandomCell(2);
        }
        if (targetCell.Occupant == null)
            return (true, targetCell);
        return (false, null);
    }
    /// <summary>Gets a random Cell on the grid
    /// </summary>
    public static Cell GetRandomCell(int marginFromEdge = 2)
    {
        var rndX = UnityEngine.Random.Range(marginFromEdge, grid.GetLength(0) - (marginFromEdge + 1));
        var rndY = UnityEngine.Random.Range(marginFromEdge, grid.GetLength(1) - (marginFromEdge + 1));
        return grid[rndX, rndY];
    }
}