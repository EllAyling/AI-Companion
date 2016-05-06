using UnityEngine;
using System.Collections;
using System;

public class Heap<T> where T : IHeapItem<T> {

    T[] items;
    int currentHeapCount;

    public Heap(int maxHeapSize) {
        items = new T[maxHeapSize]; //Create an array of the items.
    }

    public void Add(T item)
    {
        item.HeapIndex = currentHeapCount; //Assign its index
        items[currentHeapCount] = item; //Put at the end of the array
        SortUp(item);   //Sort it up
        currentHeapCount++; //Go to the next item.
    }
    void SortUp(T item)
    {
        int parentIndex = (item.HeapIndex - 1) / 2; //Get the parent index
        while (true)
        {
            T parentItem = items[parentIndex];  //Get the parent item
            if (item.CompareTo(parentItem) > 0) //Compare our item with its parent
            {
                Swap(item, parentItem);         //If its a lower priority, swap them
            }
            else break;
        }
    }

    void Swap(T a, T b)
    {
        items[a.HeapIndex] = b;
        items[b.HeapIndex] = a;

        int aIndex = a.HeapIndex;
        a.HeapIndex = b.HeapIndex;
        b.HeapIndex = aIndex;
    }
    public int Count
    {
        get { return currentHeapCount; }
    }

    public bool Contains(T item)
    {
        return Equals(items[item.HeapIndex], item); //Check if the heap contains the item
    }

    public T RemoveFirst()
    {
        T firstItem = items[0];             //Get the root
        currentHeapCount--;                 //Minus one from the total items
        items[0] = items[currentHeapCount]; //Make the last item, be the root
        items[0].HeapIndex = 0;             //Set the index to 0
        SortDown(items[0]);                 //Sort it
        return firstItem;                   //Return the root
    }

    void SortDown(T item)
    {
        while (true)
        {
            int childIndexLeft = item.HeapIndex * 2 + 1;    //Get the left child
            int childIndexRight = item.HeapIndex * 2 + 2;   //Get the right child
            int swapIndex = 0;                              //

            if (childIndexLeft < currentHeapCount)          //If this item has a child
            {
                swapIndex = childIndexLeft;                 //Assign it to a temp var

                if (childIndexRight < currentHeapCount)     //If there's a right child
                {
                    if (items[childIndexLeft].CompareTo(items[childIndexRight]) < 0)    //See if the right child has a higher priority than the left child
                    {
                        swapIndex = childIndexRight;
                    }
                }

                if (item.CompareTo(items[swapIndex]) < 0)       //Check the child against the parent
                {
                    Swap(item, items[swapIndex]);               //Swap them if we need to
                }
                else return; //Otherwise it's in the correct position
            }
            else return; //If it hasnt got any children, return
        }
    }

}

public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex{ get; set;}
}
