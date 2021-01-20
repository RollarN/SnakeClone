using GridCell;
using System.Collections.Generic;
using UnityEngine;

///<summary> Handles the tail of the snake (position, expansion, and linerenderer)
///</summary>
[RequireComponent(typeof(CellOccupant))]
public class SnakeTailManager : MonoBehaviour
{
    private LinkedList<CellOccupant> tailLinkedList;
    private CellOccupant cellOccupant;

    private ObjectPool snakeTailPool;
    ///<summary> The previous cell of the tail node, determines where the next tail should spawn.
    ///</summary>
    private Cell previousTailCell;
    public int tailNodesToAdd;



    #region Properties
    public CellOccupant CellOccupant => cellOccupant;

    public CellOccupant lastTailNode => tailLinkedList.Last.Value;
    public int TailNodesToAdd
    {
        get => tailNodesToAdd;
        set => tailNodesToAdd = value;
    }
    #endregion Properties

    public void Start()
    {
        cellOccupant = GetComponent<CellOccupant>();
        tailLinkedList = new LinkedList<CellOccupant>();
    }
    //Debug
    void Update()
    {
        //Debug
        if (Input.GetKeyDown(KeyCode.I))
        {
            TailNodesToAdd++;
        }
    }
    ///<summary> Moves the last tailnode, or any new node, to the argument Cell. Called from SnakeHead
    ///</summary>
    /// <param name="NewFirstNodeCell"> The cell to place the first tailnode at.</param>
    public void TryUpdateTailPositions(Cell NewFirstNodeCell)
    {
        if (TailNodesToAdd > 0)
        {
            var newFirstListNode = snakeTailPool.Rent(true).GetComponent<CellOccupant>();
            TailNodesToAdd--;
            tailLinkedList.AddFirst(newFirstListNode);
            newFirstListNode.CurrentCell = NewFirstNodeCell;
        }

        else if (tailLinkedList.Count > 0)
        {
            var newFirstListNode = tailLinkedList.Last.Value;
            tailLinkedList.Remove(newFirstListNode);
            tailLinkedList.AddFirst(newFirstListNode);
            newFirstListNode.CurrentCell = NewFirstNodeCell;
        }
    }
    ///<summary> Initializes the SnakeObjectPool
    ///</summary>
    public void InitializeTailPool(CellOccupant tailPrefab)
    {
        snakeTailPool = new ObjectPool(100, tailPrefab.gameObject, new GameObject("SnakeTailParent").transform);
    }
}
