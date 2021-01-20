using GridCell;
using UnityEngine;

///<summary> The head of the snake, handles collision and calls to update tail positions
///</summary>
[RequireComponent(typeof(TimedCellMovement))]
[RequireComponent(typeof(CellOccupant))]
public class SnakeHead : MonoBehaviour, ICellCollisionHandler
{
    private TimedCellMovement cellMovementHandler;
    private CellOccupant occupant;
    private SnakeTailManager snakeTail;

    #region Properties
    public SnakeTailManager SnakeTail => snakeTail;
    public TimedCellMovement CellMovementHandler => cellMovementHandler;
    public CellOccupant CellOccupant => occupant;
    #endregion Properties

    public void Start()
    {
        snakeTail = GetComponent<SnakeTailManager>();
        occupant = GetComponent<CellOccupant>();
        cellMovementHandler = GetComponent<TimedCellMovement>();

        cellMovementHandler.OnNewCell += (Cell newCell) =>
        { //moves the head to the new cell, and then calls the tail to move behind it
            var previousCell = occupant.CurrentCell;
            occupant.CurrentCell = newCell;
            snakeTail.TryUpdateTailPositions(previousCell);
        };
    }

    //CollisionHandling
    public void TryTraverse(ref Cell targetCell)
    {
        if (targetCell.Occupant == null)
            return;

        if (targetCell.Occupant.TryGetComponent(out IMovementAffector movementAffector))
        {
            movementAffector.AttemptUpdateMovementTargetCell(ref targetCell);
            //Calls itself again in case it moves to another MovementAffector
            TryTraverse(ref targetCell);
            return;
        }

        if (targetCell.Occupant.TryGetComponent(out ISnakeTraversable traversable))
        {
            traversable.OnTraversal(this);
            return;
        }

        if ((SnakeTail.lastTailNode != null) && (snakeTail.lastTailNode == targetCell.Occupant))
        {
            //Removes the last node from the cell, to prohibit an overlap
            //The tails position is set once again on next move.
            SnakeTail.lastTailNode.CurrentCell = null;
            return;
        }
        targetCell = CellOccupant.CurrentCell;
        GameLoopUtility.OnGameEnd?.Invoke();
    }
}
