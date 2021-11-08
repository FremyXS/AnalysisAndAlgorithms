using System;
using System.Collections;
using System.Diagnostics;
using System.IO;

namespace QueueRealization
{
    class Program
    {
        const string COMMAND_1 = "1";
        const string COMMAND_2 = "2";
        const string COMMAND_3 = "3";
        const string COMMAND_4 = "4";
        const string COMMAND_5 = "5";
        static void Main(string[] args)
        {
            ExecutionOne(File.ReadAllText(@"../../../input.txt").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            //ExecutionThree(File.ReadAllLines(@"../../../input2.txt"), "res1");
            //ExecutionThree(File.ReadAllLines(@"../../../input3.txt"), "res2");
        }
        private static void ExecutionOne(string[] commands)
        {
            var queue = new Queue<object>();
            Console.WriteLine("START");

            foreach (var el in commands)
            {
                ExecutionOneCommand(el, queue);
            }

            Console.WriteLine("END");
        }
        private static void ExecutionOneCommand(string command, Queue<object> queue)
        {
            if (command.Length > 1)
            {
                var cmd = command.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                queue.Enqueue(cmd[1]);
                Console.WriteLine("queue add {0}", cmd[1]);
            }
            else
            {
                switch (command)
                {
                    case COMMAND_2:
                        Console.WriteLine("queue del {0}", queue.Peek());
                        queue.Dequeue();
                        break;
                    case COMMAND_3:
                        queue.Peek();
                        Console.WriteLine("queue top {0}", queue.Peek());
                        break;
                    case COMMAND_4:
                        Console.WriteLine("queue isempty {0}", queue.IsEmpty());
                        queue.IsEmpty();
                        break;
                    case COMMAND_5:
                        Console.WriteLine("queue print");
                        Console.WriteLine(new string('-', 10));
                        queue.Print();
                        Console.WriteLine(new string('-', 10));
                        break;
                    default:
                        throw new Exception("Invalid Command");
                }
            }

        }
        private static void ExecutionThree(string[] commands, string save)
        {
            var timelist = new System.Collections.Generic.List<string>();
            Stopwatch timer = new();
            foreach (var el in commands)
            {
                timer.Restart();
                ExecutionOne(el.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                timer.Stop();
                timelist.Add(timer.Elapsed.Ticks.ToString());
            }
            File.WriteAllLines($@"../../../{save}", timelist.ToArray());
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
