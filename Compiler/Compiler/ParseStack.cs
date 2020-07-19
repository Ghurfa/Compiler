using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class ParseStack
    {
        private struct CacheItem
        {
            public int StartIndex;
            public object Item;
            public CacheItem(int pointer, object item)
            {
                StartIndex = pointer;
                Item = item;
            }
        }

        private IToken[] tokens;
        private int pointer = 0;
        private Stack<int> savePoints = new Stack<int>();
        private List<CacheItem> cache = new List<CacheItem>();

        public bool TryParse<T>(out T token) where T : IToken
        {
            if (pointer >= tokens.Length)
            {
                token = default;
                return false;
            }
            Save();
            while (tokens[pointer] is ITriviaToken)
            {
                pointer++;
                if (pointer >= tokens.Length)
                {
                    token = default;
                    Restore();
                    return false;
                }
            }
            if(tokens[pointer] is T tToken)
            {
                token = tToken;
                return true;
            }
            else
            {
                token = default;
                Restore();
                return false;
            }
        }

        public void Save() => savePoints.Push(pointer);
        public void Restore() => pointer = savePoints.Pop();
        public void Pop() => savePoints.Pop();
        public void CacheAndPop(object item)
        {
            cache.Add(new CacheItem(pointer, item));
            savePoints.Pop();
        }
        public bool CheckCache<T>(out T item)
        {
            foreach(CacheItem cacheItem in cache)
            {
                if(cacheItem.StartIndex == pointer && cacheItem.Item is T tItem)
                {
                    item = tItem;
                    return true;
                }
            }
            item = default;
            return false;
        }
    }
}
