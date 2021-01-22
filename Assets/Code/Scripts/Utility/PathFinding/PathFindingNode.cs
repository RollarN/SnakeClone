using System.Collections;
using UnityEngine;
using GridCell;


public class PathFindingNode : IHeapItem<PathFindingNode>
{
    private Cell NodeCell;

    public int yPosition => NodeCell.yPosition;
    public int xPosition => NodeCell.xPosition;

    private int heapIndex;
    public int HeapIndex
    {
        get => heapIndex;
        set => heapIndex = value;
    }
    public bool Walkable
    {

    }
    public int hCost;
    public int gCost;
    public int FCost => hCost + FCost;

    public int CompareTo(PathFindingNode otherNode)
    {
        return 0;
    }
}
