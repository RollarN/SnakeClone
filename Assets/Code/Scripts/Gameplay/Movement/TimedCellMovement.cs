using GridCell;
using System;
using UnityEngine;

/// <summary> Component that moves its owner, (or fires an event), in a direction based on a timer. Direction, collision, and actual movement can be handled externally.
/// </summary>
[RequireComponent(typeof(CellOccupant))]
public class TimedCellMovement : MonoBehaviour, ICellMovementHandler, ITimerTickOverrider
{
    [SerializeField]
    [Range(float.Epsilon, 30)]
    private float movementsPerSecond;

    /// <summary> If true, this component will move its occupant component by its own.
    /// </summary>
    [SerializeField]
    private bool isMovingSelf;
    /// <summary> if true, this component will fire it's OnMove Event.
    /// </summary>
    [SerializeField]
    private bool firesOnMoveAction;
    [SerializeField]
    private bool ignoreCollision = false;

    /// <summary> The direction this component will move in
    /// </summary>
    private Direction currentMovementDirection;

    private Action<Cell> onNewCell;
    private TimerComponent timerComponent;

    private CellOccupant cellOccupant;
    private ICellCollisionHandler collisionHandler;

    #region Properties
    public TimerComponent TimerComponent => timerComponent;

    public Direction CurrentMovementDirection
    {
        get => currentMovementDirection;
        set => currentMovementDirection = value;
    }
    public float MovementsPerSecond
    {
        get => movementsPerSecond;
        set => movementsPerSecond = value;
    }
    public float TickIntervalTime => 1 / movementsPerSecond;

    public Action<Cell> OnNewCell
    {
        get => onNewCell;
        set => onNewCell = value;
    }

    #endregion Properties
    void Start()
    {
        cellOccupant = GetComponent<CellOccupant>();
        collisionHandler = GetComponent<ICellCollisionHandler>();

        timerComponent = gameObject.AddTimerComponentOverriden(this);
        timerComponent.OnTimerTick += TryTraverse;
    }
    /// <summary> Attempts to move Object in the specified direction
    /// </summary>
    private void TryTraverse()
    {
        var targetCell = cellOccupant.CurrentCell.Neighbours[(int)CurrentMovementDirection];

        if (!ignoreCollision || collisionHandler != null)
            collisionHandler.TryTraverse(ref targetCell);

        if (targetCell != cellOccupant.CurrentCell)
        {
            var previousCell = cellOccupant.CurrentCell;
            if (firesOnMoveAction)
                OnNewCell?.Invoke(targetCell);

            if (isMovingSelf)
                cellOccupant.CurrentCell = targetCell;
        }
    }

}
