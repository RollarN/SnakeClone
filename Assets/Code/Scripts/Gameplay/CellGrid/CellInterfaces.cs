using GridCell;
using System;
/// <summary>Interface that implements TryTraverse, enabling Collisionhandling
/// </summary>
public interface ICellCollisionHandler
{
    /// <summary>Attempts to traverse the targetnode, and updates the argument to the resulting node
    /// </summary>
    void TryTraverse(ref Cell targetCell);
}
/// <summary>Interface that implements OnNewCell Action, which should Invoke every time a move is made
/// </summary>
public interface ICellMovementHandler
{
    /// <summary>Action that should invoke whenever a move is made on the grid. Argument is the new Cell.
    /// </summary>
    Action<Cell> OnNewCell
    {
        get;
        set;
    }
}
