using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLinkedList<T>
{
    public ListNode<T> head;
    public ListNode<T> body;
    public ListNode<T> tail;
    private ListNode<T> selected;

    private int count = 0;

    public int Count { get { return count; } }

    public class ListNode<T> // The node for the linked list
    {
        public T data;
        public ListNode<T> next;

        public ListNode(T part)
        {
            part = data;
            next = null;
        }

    }
    public void AddLast(T part) // Adds a head if not existing and sets it as the head and tail, if a head exists the new part becomes the tail and increases the count
    {

        if (head == null && Count == 0)
        {
            count++;
            head = new ListNode<T>(part) { data = part };
            tail = head;
        }
        else
        {
            count++;
            tail.next = new ListNode<T>(part) { data = part };
            tail = tail.next;
        }



    }

    public T GetAtIndex(int index) // Gets the object at a specific index
    {
        selected = head;
        for(int i = 0; i < index; i++)
        {
            selected = selected.next;

            if(i == index)
            {
                return selected.data;
            }
        }
        return selected.data;
    }

    public T Remove(int index) // Clears the next of the selected part
    {
        T tempPart = default;
        selected = head;
        for(int i = 0; i < index; i++)
        {
            selected = selected.next;
            if(i == index)
            {
                tempPart = selected.next.data;
                selected.next = null;
            }
        }
        return tempPart;
    }



}
