using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryClass;

namespace HashTable
{
    public class MyHashTable<T> where T : IInit, ICloneable, new()
    {
        Point<T>?[] table;
        public int Capacity => table.Length;
        public MyHashTable(int length = 10) 
        {
            table = new Point<T>[length];
        }

        public void PrintTable() 
        {
            for (int i = 0; i < table.Length; i++) 
            {
                Console.WriteLine($"{i}:");
                if (table[i]!= null ) 
                {
                    Console.WriteLine(table[i].Data);
                    if (table[i].Next != null) 
                    {
                        Point<T>? current = table[i].Next;
                        while (current != null) 
                        {
                            Console.WriteLine(current.Data);
                            current = current.Next;
                        }
                    }
                }
            }
        }

        public void AddPoint(T data)
        {
            int index = GetIndex(data);
            if (table[index] == null)
            {
                table[index] = new Point<T>(data);
            }
            else
            {
                Point<T>? current = table[index];
                while (current.Next != null)
                {
                    if (current.Data.Equals(data))
                        return;
                    current = current.Next;
                }
                current.Next = new Point<T>(data);
                current.Next.Pred = current;
            }
        }

        public bool Contains(T data) 
        {
            int index = GetIndex(data);
            if (table == null)
                throw new Exception("empty table");
            if (table[index] == null)
                return false;
            if (table[index].Data.Equals(data))
                return true;
            else 
            {
                Point<T> current = table[index];
                while (current != null) 
                {
                    if (current.Data.Equals(data))
                        return true;
                    current = current.Next;
                }
            }
            return false;
        }

        public bool RemoveData(T data) 
        {
            Point<T> current;
            int index = GetIndex(data);
            if (table[index] == null)
                return false;
            if (table[index].Data.Equals(data))
            {
                if (table[index].Next == null)
                    table[index] = null;
                else 
                {
                    table[index] = table[index].Next;
                    table[index].Pred = null;
                }
                   
                return true;
            }
            else 
            {
                current = table[index];
                while (current != null) 
                {
                    if (current.Data.Equals(data)) 
                    {
                        Point<T>? pred = current.Pred;
                        Point<T>? next = current.Next;
                        pred.Next = next;
                        current.Pred = null;
                        if (next != null)
                            next.Pred = pred;
                        return true;
                    }
                    current = current.Next;
                }
            }
            return false;
        }

        public bool AddAfter(T existingData, T newData)
        {
            int index = GetIndex(existingData);
            if (table[index] == null)
                return false;

            Point<T> current = table[index];
            while (current != null)
            {
                if (current.Data.Equals(existingData))
                {
                    Point<T> newNode = new Point<T>(newData);
                    newNode.Next = current.Next;
                    if (current.Next != null)
                    {
                        current.Next.Pred = newNode;
                    }
                    newNode.Pred = current;
                    current.Next = newNode;
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public int GetIndex(T data) 
        {
            return Math.Abs(data.GetHashCode()) % Capacity;
        }
    }
}