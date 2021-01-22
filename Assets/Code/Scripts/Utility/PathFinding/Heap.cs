using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;
using GridCell;

public class Heap<T> where T : IHeapItem<T>
{
    T[] items;
    int currentItemCount;
    public Heap(int HeapSize)
    {
        items = new T[HeapSize];
    }
    public void Add(T item)
    {
        items[currentItemCount] = item;
        item.HeapIndex = currentItemCount;
        currentItemCount++;
        SortUp(item);
    }
    private void SortUp(T item)
    {
        var parentIndex = (item.HeapIndex - 1) / 2;
        while (true)
        {
            T parentItem = items[parentIndex];
            if(item.CompareTo(parentItem) > 0)
            {
                Swap(item, parentItem);
            }
        }
    }

    /// <summary>Removes the first item of the heap, does a sortdown of the last.
    /// </summary>
    public T RemoveFirst()
    {
        T firstItem = items[0];
        currentItemCount--;
        items[0] = items[currentItemCount];
        items[0].HeapIndex = 0;
        SortDown(items[0]);
        return firstItem;
    }
    /// <summary>Compares the childIndex
    /// </summary>
    private void SortDown(T item)
    {
        while (true)
        {
            var childIndexLeft = item.HeapIndex * 2 + 1;
            var childIndexRight = item.HeapIndex * 2 + 2;
            
            int swapIndex = 0;

            if(childIndexLeft < currentItemCount)
            {
                swapIndex = childIndexLeft;
                if (childIndexRight < currentItemCount)
                {
                    if(items[childIndexLeft].CompareTo(items[childIndexRight]) < 0)
                    {
                        swapIndex = childIndexRight;
                    } 
                }
                if (item.CompareTo(items[swapIndex]) < 0)
                {
                    Swap(item, items[swapIndex]);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
        
    }
    
    public bool Contains(T item) => Equals(items[item.HeapIndex], item);

    private void Swap(T itemA, T itemB)
    {
        items[itemA.HeapIndex] = itemB;
        items[itemB.HeapIndex] = itemA;
        var itemAIndex = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = itemAIndex;
    }
}

public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex
    {
        get;
        set;
    }
}