using GridCell;
using UnityEngine;

/// <summary>Dataholder containing data about the grid, as well as the logic to generate it.
/// </summary>
[CreateAssetMenu(fileName = "newLevelGridData", menuName = "ScriptableObjects/LevelData/LevelGridData", order = 1)]
public class LevelGridData : ScriptableObject
{
    [SerializeField]
    private int snakeStartPositionX;
    [SerializeField]
    private int snakeStartPositionY;

    [SerializeField]
    private int gridSizeY;
    [SerializeField]
    [Range(0.5f, 1.778f)]
    private float gridSizeXMultiplier;
    [SerializeField]
    private CellOccupant edgeCellOccupant;

    #region Properties
    public int GridSizeY => gridSizeY;
    public int GridSizeX => Mathf.FloorToInt(gridSizeY * gridSizeXMultiplier);

    public CellOccupant EdgeCellOccupant => edgeCellOccupant;

    public int SnakeStartPositionX => (int)Mathf.Clamp(snakeStartPositionX, 1, GridSizeX);
    public int SnakeStartPositionY => (int)Mathf.Clamp(snakeStartPositionY, 1, GridSizeY);

    #endregion Properties
    /// <summary> Generates and return the Grid based on the objects' data. Should only be called once in InitializeGame
    ///</summary>
    public Cell[,] GenerateGrid()
    {
        Cell[,] Grid = new Cell[GridSizeX, GridSizeY];
        for (int x = 0; x < GridSizeX; x++)
        {
            for (int y = 0; y < GridSizeY; y++)
            {
                Grid[x, y] = new Cell(x, y);
            }
        }

        #region Edge&NeighbourSetup
        bool IsGridEdge(int x, int y)
        {
            return (
                (y == GridSizeY - 1) ||
                (x == GridSizeX - 1) ||
                (y == 0) ||
                (x == 0)
            );
        }
        Transform edgeCellParent = new GameObject("EdgeCellParent").transform;
        for (int x = 0; x < GridSizeX; x++)
        {
            for (int y = 0; y < GridSizeY; y++)
            {
                if (IsGridEdge(x, y))
                {
                    var newEdgeCellOccupant = Instantiate(EdgeCellOccupant, edgeCellParent);
                    newEdgeCellOccupant.CurrentCell = Grid[x, y];
                    newEdgeCellOccupant.GetComponent<ISpecialGridEdge>()?.PlaceOnGrid(Grid);
                    continue;
                }

                if (y + 1 < GridSizeY)
                    Grid[x, y].Neighbours[(int)Direction.Up] = Grid[x, y + 1];

                if (x + 1 < GridSizeX)
                    Grid[x, y].Neighbours[(int)Direction.Right] = Grid[x + 1, y];

                if (x > 0)
                    Grid[x, y].Neighbours[(int)Direction.Left] = Grid[x - 1, y];

                if (y > 0)
                    Grid[x, y].Neighbours[(int)Direction.Down] = Grid[x, y - 1];
            }
        }
        #endregion Edge&NeighbourSetup
        return Grid;
    }

}
/// <summary> Interface for components with special interactions when placed on a grid
///</summary>
public interface ISpecialGridEdge
{
    /// <summary> Applies a special interaction when placed on the target grid
    ///</summary>
    void PlaceOnGrid(Cell[,] grid);
}
