using System;
using UnityEngine;

/// <summary> Component that allows player input to control TimedCellMovementComponent. Can only turn 90 degrees per movement Tick
/// </summary>
[RequireComponent(typeof(TimedCellMovement))]
public class PlayerTimedMovementController : MonoBehaviour
{
    private TimedCellMovement movementComponent;
    private Direction previousDirection;

    void Start()
    {
        movementComponent = GetComponent<TimedCellMovement>();

        //keeps data of the MovementComponents previous direction
        movementComponent.TimerComponent.OnTimerTick += () => previousDirection = movementComponent.CurrentMovementDirection;
    }

    void Update() => UpdateDirection();

    /// <summary>Sets the Movementdirection of the TimedCellMovementComponent
    /// </summary>
    private void UpdateDirection()
    {
        //iterates through every direction
        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            //Checks if player is pushing current direction's button, and if it's viable turn
            if (Input.GetButtonDown(direction.ToString()) &&
                direction.IsPerpendicularTo(previousDirection))
            {
                //sets direction
                movementComponent.CurrentMovementDirection = direction;
                break;
            }
        }
    }
}

