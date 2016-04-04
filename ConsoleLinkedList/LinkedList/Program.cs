using System;
using System.Linq;

namespace LinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[5] { 2, 4, 5, 1, 3 };
            BubbleSort(ref array);

            LinkedList linkedList = new LinkedList(array[0]);

            for (int i = 1; i < array.Count(); i++)
            {
                linkedList.Add(array[i]);
            }

            linkedList.Print();
            Console.ReadKey();
        }

        static void BubbleSort(ref int[] A)
        {
            for (int i = 0; i < A.Length; i++)
            {
                for (int j = 0; j < A.Length - 1; j++)
                {
                    if (A[j] > A[j + 1])
                    {
                        int z = A[j];
                        A[j] = A[j + 1];
                        A[j + 1] = z;
                    }
                }
            }
        }

        public class LinkedList
        {
            public Node head;

            public LinkedList(int initial)
            {
                head = new Node(null, initial);
            }


            public void Add(int value)
            {
                Node current = head;

                while (current.next != null)
                {
                    current = current.next;
                }

                current.next = new Node(null, value);
            }

            public void Print()
            {
                Node current = head;

                while (current != null)
                {
                    Console.WriteLine(current.value);
                    current = current.next;
                }
            }
        }

        public class Node
        {
            public Node(Node next, int value)
            {
                this.next = next;
                this.value = value;
            }

            public Node next;
            public int value;
        }
    }
}
