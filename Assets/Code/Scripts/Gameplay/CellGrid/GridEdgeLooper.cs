using GridCell;
using UnityEngine;
[RequireComponent(typeof(CellOccupant))]

/// <summary> An occupant that puts the snake on the opposite side of the map when traversed 
///</summary>
public class GridEdgeLooper : MonoBehaviour, ISpecialGridEdge, IMovementAffector
{
    /// <summary> The cell to teleport the snake to when traversed
    /// </summary>
    private Cell teleportTargetCell;

    /// <summary> Finds a cell on the the opposite side of the grid and assigns that as the teleportCell
    /// </summary>
    public void PlaceOnGrid(Cell[,] Grid)
    {
        var cellOccupant = GetComponent<CellOccupant>();
        var xPos = cellOccupant.CurrentCell.xPosition;
        var yPos = cellOccupant.CurrentCell.yPosition;

        if (xPos == Grid.GetLength(0) - 1)
        {
            teleportTargetCell = Grid[1, yPos];
            return;
        }

        if (xPos == 0)
        {
            teleportTargetCell = Grid[Grid.GetLength(0) - 2, yPos];
            return;
        }

        if (yPos == 0)
        {
            teleportTargetCell = Grid[xPos, Grid.GetLength(1) - 2];
            return;
        }

        if (yPos == Grid.GetLength(1) - 1)
        {
            teleportTargetCell = Grid[xPos, 1];
            return;
        }
    }
    public void AttemptUpdateMovementTargetCell(ref Cell targetCell)
    {
        targetCell = teleportTargetCell;
    }

}
