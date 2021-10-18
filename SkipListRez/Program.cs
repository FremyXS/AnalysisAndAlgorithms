using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.IO;

namespace SkipListRez
{
    class Program
    {
        private static List<double> Res = new List<double>();
        private static List<double> Res2 = new List<double>();

        static void Main(string[] args)
        {
            Progon();
            WriteData();
        }
        private static void Progon()
        {
            var array = GetArray();

            //for (int i = 0; i < 2000; i++)
            //    Execute(array, i);

            for (int i = 0; i < 2000; i++)
                InsSort(SetArrays(array, i));
        }
        private static void Execute(int[] array, int N)
        {
            AVL tree = GetTree(array, N);

            Stopwatch timer = new();
            timer.Restart();

            for (int i = 0; i < 5; i++)
                Function(tree);

            timer.Stop();
            var res = timer.Elapsed;

            Res.Add(res.Ticks);
        }
        private static void WriteData()
        {
            //File.WriteAllText(@"../../../avltree7.csv", GetStr("AvlTree; ;", Res));
            File.WriteAllText(@"../../../insertsort.csv", GetStr("insertSort; ;", Res2));
        }
        private static string GetStr(string name, List<double> res)
        {
            string str = name;
            foreach(var el in res)
            {
                str += el.ToString() + ';';
            }

            return str;
        }
        private static void Function(AVL tree)
        {
            
            tree.DisplayTree();
        }
        private static AVL GetTree(int[] array, int N)
        {
            AVL tree = new AVL();

            for (int i = 0; i <= N; i++)
                tree.Add(array[i]);

            return tree;
        }
        private static int[] GetArray()
        {
            var array = new int[2000];            

            for (int i = 0; i < 2000; i++)
            {
                bool t = true;

                while (t)
                {
                    int num = new Random().Next(3000);

                    if (!array.Contains(num))
                    {
                        array[i] = num;
                        t = false;
                    }
                }
            }

            return array;
            
        }
        private static List<int[]> SetArrays(int[] array, int N)
        {
            var array2 = new int[N + 1];

            for (int i = 0; i <= N; i++)
                array2[i] = array[i];

            return new List<int[]>()
            {
                (int[])array2.Clone(),
                (int[])array2.Clone(),
                (int[])array2.Clone(),
                (int[])array2.Clone(),
                (int[])array2.Clone()
            };

        }
        static void Swap(ref int e1, ref int e2)
        {
            var temp = e1;
            e1 = e2;
            e2 = temp;
        }

        //сортировка вставками
        static void InsSort(List<int[]> arrays)
        {
            Stopwatch timer = new();
            timer.Restart();

            for (int i = 0; i < 5; i++)
            {
                InsertionSort(arrays[0]);
            }

            timer.Stop();
            var res = timer.Elapsed;

            Res2.Add(res.Ticks);
        }

        static int[] InsertionSort(int[] array)
        {
            for (var i = 1; i < array.Length; i++)
            {
                var key = array[i];
                var j = i;
                while ((j > 1) && (array[j - 1] > key))
                {
                    Swap(ref array[j - 1], ref array[j]);
                    j--;
                }

                array[j] = key;
            }

            return array;
        }
    }
    class AVL
    {
        class Node
        {
            public int data;
            public Node left;
            public Node right;
            public Node(int data)
            {
                this.data = data;
            }
        }
        Node root;
        public AVL()
        {
        }
        public void Add(int data)
        {
            Node newItem = new Node(data);
            if (root == null)
            {
                root = newItem;
            }
            else
            {
                root = RecursiveInsert(root, newItem);
            }
        }
        private Node RecursiveInsert(Node current, Node n)
        {
            if (current == null)
            {
                current = n;
                return current;
            }
            else if (n.data < current.data)
            {
                current.left = RecursiveInsert(current.left, n);
                current = balance_tree(current);
            }
            else if (n.data > current.data)
            {
                current.right = RecursiveInsert(current.right, n);
                current = balance_tree(current);
            }
            return current;
        }
        private Node balance_tree(Node current)
        {
            int b_factor = balance_factor(current);
            if (b_factor > 1)
            {
                if (balance_factor(current.left) > 0)
                {
                    current = RotateLL(current);
                }
                else
                {
                    current = RotateLR(current);
                }
            }
            else if (b_factor < -1)
            {
                if (balance_factor(current.right) > 0)
                {
                    current = RotateRL(current);
                }
                else
                {
                    current = RotateRR(current);
                }
            }
            return current;
        }
        public void Delete(int target)
        {
            root = Delete(root, target);
        }
        private Node Delete(Node current, int target)
        {
            Node parent;
            if (current == null)
            { return null; }
            else
            {
                if (target < current.data)
                {
                    current.left = Delete(current.left, target);
                    if (balance_factor(current) == -2)
                    {
                        if (balance_factor(current.right) <= 0)
                        {
                            current = RotateRR(current);
                        }
                        else
                        {
                            current = RotateRL(current);
                        }
                    }
                }
                else if (target > current.data)
                {
                    current.right = Delete(current.right, target);
                    if (balance_factor(current) == 2)
                    {
                        if (balance_factor(current.left) >= 0)
                        {
                            current = RotateLL(current);
                        }
                        else
                        {
                            current = RotateLR(current);
                        }
                    }
                }
                else
                {
                    if (current.right != null)
                    {
                        parent = current.right;
                        while (parent.left != null)
                        {
                            parent = parent.left;
                        }
                        current.data = parent.data;
                        current.right = Delete(current.right, parent.data);
                        if (balance_factor(current) == 2)
                        {
                            if (balance_factor(current.left) >= 0)
                            {
                                current = RotateLL(current);
                            }
                            else { current = RotateLR(current); }
                        }
                    }
                    else
                    {   
                        return current.left;
                    }
                }
            }
            return current;
        }
        public void Find(int key)
        {
            if (Find(key, root).data == key)
            {
                Console.WriteLine("{0} was found!", key);
            }
            else
            {
                Console.WriteLine("Nothing found!");
            }
        }
        private Node Find(int target, Node current)
        {

            if (target < current.data)
            {
                if (target == current.data)
                {
                    return current;
                }
                else
                    return Find(target, current.left);
            }
            else
            {
                if (target == current.data)
                {
                    return current;
                }
                else
                    return Find(target, current.right);
            }

        }
        public void DisplayTree()
        {
            if (root == null)
            {
                Console.WriteLine("Tree is empty");
                return;
            }
            InOrderDisplayTree(root);
            Console.WriteLine();
        }
        private void InOrderDisplayTree(Node current)
        {
            if (current != null)
            {
                InOrderDisplayTree(current.left);
                //Console.Write("({0}) ", current.data);
                InOrderDisplayTree(current.right);
            }
        }
        private int max(int l, int r)
        {
            return l > r ? l : r;
        }
        private int getHeight(Node current)
        {
            int height = 0;
            if (current != null)
            {
                int l = getHeight(current.left);
                int r = getHeight(current.right);
                int m = max(l, r);
                height = m + 1;
            }
            return height;
        }
        private int balance_factor(Node current)
        {
            int l = getHeight(current.left);
            int r = getHeight(current.right);
            int b_factor = l - r;
            return b_factor;
        }
        private Node RotateRR(Node parent)
        {
            Node pivot = parent.right;
            parent.right = pivot.left;
            pivot.left = parent;
            return pivot;
        }
        private Node RotateLL(Node parent)
        {
            Node pivot = parent.left;
            parent.left = pivot.right;
            pivot.right = parent;
            return pivot;
        }
        private Node RotateLR(Node parent)
        {
            Node pivot = parent.left;
            parent.left = RotateRR(pivot);
            return RotateLL(parent);
        }
        private Node RotateRL(Node parent)
        {
            Node pivot = parent.right;
            parent.right = RotateLL(pivot);
            return RotateRR(parent);
        }
    }
}
