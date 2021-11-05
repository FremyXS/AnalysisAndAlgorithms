using System;
using System.Diagnostics;
using System.IO;

namespace StackRealization
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
            //ExecutionOne(File.ReadAllText(@"../../../input.txt").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            //ExecutionTwo("(2+2^2)-1*9");
            ExecutionThree(File.ReadAllLines(@"../../../input2.txt"));
        }
        private static void ExecutionOne(string[] commands)
        {
            var stack = new Stack<object>();

            Console.WriteLine("START");

            foreach(var el in commands)
            {
                ExecutionOneCommand(el, stack);
            }

            Console.WriteLine("END");
        }
        private static void ExecutionOneCommand(string command, Stack<object> stack)
        {
            if(command.Length > 1)
            {
                var cmd = command.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                stack.Push(cmd[1]);
                Console.WriteLine("stack add {0}", cmd[1]);
            }
            else
            {
                switch (command)
                {
                    case COMMAND_2:
                        Console.WriteLine("stack del {0}", stack.Peek());
                        stack.Pop();                        
                        break;
                    case COMMAND_3:
                        stack.Peek();
                        Console.WriteLine("stack top {0}", stack.Peek());
                        break;
                    case COMMAND_4:
                        Console.WriteLine("stack isempty {0}", stack.IsEmpty());
                        stack.IsEmpty();
                        break;
                    case COMMAND_5:
                        Console.WriteLine("stack print");
                        Console.WriteLine(new string('-', 10));
                        stack.Print();
                        Console.WriteLine(new string('-', 10));
                        break;
                    default:
                        throw new Exception("Invalid Command");
                }
            }

        }
        private static void ExecutionTwo(string function)
        {
            var stack = new Stack<object>();
            var postFix = GetPostfix(function, stack);

            Console.WriteLine(function);
            Console.WriteLine(new string('-', 10));
            postFix.Print();

            CalculatePostfix(postFix, stack);

            Console.WriteLine(new string('-', 10));
            Console.WriteLine(stack.Peek());
        }
        private static Stack<object> GetPostfix(string function, Stack<object> stackOne)
        {
            var postFix = new Stack<object>();

            foreach (var el in function)
            {
                CheckLetter(el, stackOne, postFix);
            }

            while (stackOne.Count != 0)
            {
                postFix.Push(stackOne.Pop());
            }

            var rez = new Stack<object>();

            while(postFix.Count != 0)
            {
                rez.Push(postFix.Pop());
            }

            return rez;
        }
        private static void CheckLetter(char let, Stack<object> stackOne, Stack<object> postFix)
        {
            if(IsOperation(let))
            {
                if(stackOne.Count == 0)
                {
                    stackOne.Push(let);
                }
                else
                {
                    if(GetPrior(let) > GetPrior((char)stackOne.Peek()))
                    {
                        stackOne.Push(let);
                    }
                    else if((GetPrior(let) == GetPrior((char)stackOne.Peek())) && let == new Degree().Name)
                    {
                        stackOne.Push(let);
                    }
                    else
                    {
                        while((stackOne.Count != 0) && (GetPrior(let) <= GetPrior((char)stackOne.Peek())))
                        {
                            postFix.Push(stackOne.Pop());
                        }

                        stackOne.Push(let);
                    }
                }
            }            
            else if (let == new LeftPair().Name)
            {
                stackOne.Push(let);
            }
            else if (let == new RightPair().Name)
            {
                while(stackOne.Count != 0)
                {
                    if((char)stackOne.Peek() == new LeftPair().Name)
                    {
                        stackOne.Pop();
                        break;
                    }

                    postFix.Push(stackOne.Pop());
                }
            }
            else
            {
                postFix.Push(int.Parse(let.ToString()));
            }
        }
        private static bool IsOperation(char let)
        {
            if (let == new Plus().Name || let == new Minus().Name
            || let == new Mult().Name || let == new Div().Name || let == new Degree().Name)
                return true;
            else
                return false;
        }
        private static int GetPrior(char let)
        {
            if (let == new Plus().Name)
                return new Plus().Priority;
            else if (let == new Minus().Name)
                return new Minus().Priority;
            else if (let == new Mult().Name)
                return new Mult().Priority;
            else if (let == new Div().Name)
                return new Div().Priority;
            else if (let == new Degree().Name)
                return new Degree().Priority;
            else
                return -1;

        }
        private static void CalculatePostfix(Stack<object> postfix, Stack<object> rez)
        {
            while (postfix.Count != 0)
            {
                var let = postfix.Pop();

                if (let is int)
                {
                    rez.Push((int)let);
                }
                else
                {
                    rez.Push(GetResult(new int[] { (int)rez.Pop(), (int)rez.Pop() }, (char)let));
                }
            }
        }
        private static int GetResult(int[] nums, char op)
        {
            if (op == new Plus().Name)
                return new Plus().Evaluate(nums);
            else if (op == new Minus().Name)
                return new Minus().Evaluate(nums);
            else if (op == new Mult().Name)
                return new Mult().Evaluate(nums);
            else if (op == new Div().Name)
                return new Div().Evaluate(nums);
            else if (op == new Degree().Name)
                return new Degree().Evaluate(nums);

            throw new Exception("Invalid Operation");
        }
        private static void ExecutionThree(string[] commands)
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
            File.WriteAllLines(@"../../../res.csv", timelist.ToArray());
        }
    }        
    public class Stack<T>
    {
        private StackItem Head;
        public int Count { get; private set; } = 0;
        public Stack() { }
        public void Push (T value)
        {
            var stackItem = new StackItem(value);
            stackItem.Next = Head;
            Head = stackItem;
            Count++;
        }
        public T Pop()
        {
            if (Head is null) 
                throw new InvalidOperationException("Stack Empty");

            var res = Head.Value;
            Head = Head.Next;
            Count--;

            return res;
        }
        public T Peek()
        {
            if (Head is null)
                throw new InvalidOperationException("Stack Empty");

            return Head.Value;
        }
        public bool IsEmpty()
            => Head == null;
        public void Print()
        {
            if (Head.Next is not null)
                GetItem(Head);            
        }
        private void GetItem(StackItem stackItem)
        {
            Console.WriteLine(stackItem.Value);
            if (stackItem.Next is not null)
                GetItem(stackItem.Next);
        }

        private class StackItem
        {
            public T Value { get; }
            public StackItem Next { get; set; } = null;
            public StackItem(T value)
            {
                Value = value;
            }
            public StackItem() { }
        }
    }
}
