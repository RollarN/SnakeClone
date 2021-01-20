using GridCell;
using UnityEngine;

/// <summary>Component that allows it's owner to occupy a cell on the grid. frees up it's cell whenever it's disabled
/// </summary>
public class CellOccupant : MonoBehaviour
{
    private Cell currentCell;

    /// <summary>The cell currently occupied by this Occupant
    /// </summary>
    public Cell CurrentCell
    {
        get => currentCell ?? (currentCell = transform.position.VectorToCell());

        set
        {
            if (currentCell == value)
                return;

            if (currentCell != null)
            {
                currentCell.Occupant = null;
                currentCell = null;
            }

            if (value != null)
            {
                value.Occupant = this;
                transform.position = value.Vector2Position;
            }
            currentCell = value;
        }
    }
    private void OnDisable()
    {
        if (currentCell != null)
            currentCell.Occupant = null;
        currentCell = null;
    }
}
