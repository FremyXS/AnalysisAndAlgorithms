using System;
using System.Collections;

namespace QueueRealization
{
    class Program
    {
        static void Main(string[] args)
        {
            var q = new Queue<int>();

            var t = new System.Collections.Generic.Queue<object>();
            Console.WriteLine(t.GetEnumerator());
        }
    }
    public class Queue<T> 
    {
        private QueueItem Tail;
        private QueueItem Head;
        public int Count { get; private set; } = 0;
        public void Enqueue(T value)
        {
            var item = new QueueItem(value);

            if (Head is null)
            {
                Head = Tail = item;
            }
            else
            {
                Tail.Next = item;
                Tail = Tail.Next;
            }

            Count++;
        }
        public T Dequeue()
        {
            if (Head is null) 
                throw new Exception("Queue is Empty");

            var res = Head.Value;
            Head = Head.Next;

            if (Head is null)
                Tail = null;

            Count--;

            return res;
        }
        public T Peek()
        {
            if (Head is null)
                throw new Exception("Queue is Empty");

            return Head.Value;
        }
        public bool IsEmpty()
            => Head == null;
        public void Print()
        {
            Console.WriteLine(Head.Value);
            if (Head.Next is not null)
                GetItem(Head.Next);
        }
        private void GetItem(QueueItem item)
        {
            Console.WriteLine(item.Value);
            if (item.Next is not null)
                GetItem(item.Next);
        }


        public Queue() { }        
        
        private class QueueItem
        {
            public T Value { get; }
            public QueueItem Next { get; set; } = null;
            public QueueItem(T value)
            {
                Value = value;
            }
        }
    }
}
