using UnityEngine;

namespace GridCell
{

    /// <summary>Cell on the Grid. Has information about its position, occupant and neighbours.
    /// </summary>
    public class Cell
    {
        public int yPosition;
        public int xPosition;

        public Cell[] Neighbours;

        private CellOccupant occupant;

        /// <summary>Occupant currently placed on the Cell
        /// </summary>
        public CellOccupant Occupant
        {
            get => occupant;
            set
            {
                if (occupant != null && value != null)
                {
                    Debug.LogError("An occupant was overwritten: " +
                        occupant.gameObject.name + " overwritten by: " + value +
                        ". Cell position (X: " + xPosition + " Y: " + yPosition + ").");
                }
                occupant = value;
            }
        }

        public Cell(int newXPosition, int newYPosition)
        {
            yPosition = newYPosition;
            xPosition = newXPosition;
            Neighbours = new Cell[4];
        }
        public Vector2 Vector2Position => new Vector2(xPosition, yPosition);
        public Vector3 Vector3Position => Vector2Position;
    }
}
