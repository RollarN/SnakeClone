using GridCell;
/// <summary> Interface for Occupants with special interactions when the snake tries to traverse them
/// </summary>
public interface ISnakeTraversable
{   /// <summary> Applies an effect to the snake when traversed
    /// </summary>
    void OnTraversal(SnakeHead snake);
}
/// <summary> Interface for Occupants which can affect the snake's movement when traversed
/// </summary>
public interface IMovementAffector
{
    /// <summary> Attempts to Update the targetCell variable
    /// </summary>
    void AttemptUpdateMovementTargetCell(ref Cell targetCell);
}


