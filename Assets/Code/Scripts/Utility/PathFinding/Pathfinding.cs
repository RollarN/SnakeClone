using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridCell;

public class Pathfinding : MonoBehaviour
{
    private Dictionary<Cell, (int hCost, int gCost, Cell parent)> CellPathVariables;

    int GetHCost(Cell cell) => CellPathVariables[cell].hCost;
    int GetGCost(Cell cell) => CellPathVariables[cell].gCost;
    int GetFCost(Cell cell) => GetHCost(cell) + GetGCost(cell);

    
    void Findpath(Cell firstCell, Cell TargetCell)
    {
        List<Cell> openSet = new List<Cell>();
        HashSet<Cell> closedSet = new HashSet<Cell>();
        openSet.Add(firstCell);

        while(openSet.Count > 0)
        {
            Cell currentCell = openSet[0];
            for(int i = 1; i < openSet.Count; i++)
            {
                if((GetFCost(openSet[i]) < GetFCost(currentCell)) 
                    || (GetFCost(openSet[i]) == GetFCost(currentCell) 
                    && (GetHCost(openSet[i]) < GetHCost(currentCell))))
                {
                    currentCell = openSet[i];
                }

                //if currentCell has <= hcost & fCost of Openset[i], use that as currentNode. dont break
            }
            //Move currentCell to Closed
            openSet.Remove(currentCell);
            closedSet.Remove(currentCell);

            if (currentCell == TargetCell)
                return;

            for(int i = 0; i < currentCell.Neighbours.Length; i++)
            {
                var curNeighbour = currentCell.Neighbours[i];
                if (closedSet.Contains(curNeighbour))
                    continue;

                var newMovementCostToNeighbour = GetGCost(currentCell) + GetDistance(currentCell, curNeighbour); 
                if((newMovementCostToNeighbour < GetGCost(curNeighbour))
                    || !openSet.Contains(curNeighbour)){

                    CellPathVariables[curNeighbour] = (newMovementCostToNeighbour, CellPathVariables[curNeighbour].gCost, currentCell);

                    if (!openSet.Contains(curNeighbour))
                        openSet.Add(curNeighbour);
                }
            }
        }
    }


    List<Cell> RetracePath(Cell startCell, Cell endCell)
    {
        List<Cell> path = new List<Cell>();
        Cell currentCell = endCell;
        while(currentCell != startCell)
        {
            path.Add(currentCell);
            currentCell = CellPathVariables[currentCell].parent;
        }
        path.Reverse();
        return path;
    }

    int GetDistance(Cell from, Cell To)
    {
        int distanceX = (int)Mathf.Abs(from.xPosition - To.xPosition);
        int distanceY = (int)Mathf.Abs(from.yPosition - To.yPosition);

        if (distanceX > distanceY)
            return 14 * distanceY + 10 * (distanceX - distanceY);
        return 14 * distanceX + 10 * (distanceX - distanceY);
    }
}
