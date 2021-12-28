using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Numerics;

namespace HashTables
{
    public class MyDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        internal class Pair
        {
            internal TValue Value;
            internal TKey Key;
            internal Pair(TKey key, TValue value)
            {
                Value = value;
                Key = key;
            }
        }
        private readonly LinkedList<Pair>[] buckets;
        public MyDictionary(int dictLenght = 200)
        {
            buckets = new LinkedList<Pair>[dictLenght];
            for (int i = 0; i < buckets.Length; i++)
                buckets[i] = new LinkedList<Pair>();
        }
        public void Add(TKey key, TValue value)
        {
            if (IsHaveValue(key)) 
                throw new Exception("This key contains in dictionary");
            var list = GetList(key);
            list.AddFirst(new Pair(key, value));
        }
        public void Remove(TKey key)
        {
            if (!IsHaveValue(key))
                throw new Exception("This key not contains in dictionary");

            var list = GetList(key);
            var value = GetKeyValuePair(key);
            list.Remove(value);
        }
        public bool IsHaveValue(TKey key)
        {
            var list = GetList(key);
            foreach (var cell in list)
                if (cell.Key?.Equals(key) ?? false)
                    return true;
            return false;
        }
        private LinkedList<Pair> GetList(TKey key)
        {
            int hash = key?.GetHashCode() ?? throw new Exception("Key mustn't be null");
            int bucketNum = (hash & 0x7fffffff) % buckets.Length;
            return buckets[bucketNum];
        }
        private Pair GetKeyValuePair(TKey key)
        {
            var list = GetList(key);
            foreach (var cell in list)
                if (cell.Key?.Equals(key) ?? false)
                    return cell;

            throw new Exception("Key not found");
        }

        public void Set(TKey key, TValue value) => GetKeyValuePair(key).Value = value;
        public TValue Get(TKey key) => GetKeyValuePair(key).Value;
        public TValue this[TKey key]
        {
            get => Get(key);
            set => Set(key, value);
        }
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var bucket in buckets)
                foreach (var pair in bucket)
                    yield return new KeyValuePair<TKey, TValue>(pair.Key, pair.Value);
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerable<List<int>> BucketsFilling()
        {
            int i = 0;
            foreach (var bucket in buckets)
            {
                yield return new List<int>() {i, bucket.Count };
                i++;
            }
        }
    }
}
