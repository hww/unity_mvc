// =============================================================================
// MIT License
// 
// Copyright (c) 2018 Valeriya Pudova (hww.github.io)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// =============================================================================

using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace VARP.MVC
{

    /// <summary>
    /// This structure allow relocate last item when one of other elements is removed
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Bucket<T> where T : class 
    {
        public readonly List<T> EntitiesCollection = new List<T>(100);
        
        /// <summary>
        /// Quantity of entities
        /// </summary>
        public int Count;

        /// <summary>
        /// Get entity at the index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T GetAt(int index)
        {
            return EntitiesCollection[index];
        }
        
        /// <summary>
        /// Add single entity to the list
        /// </summary>
        /// <param name="item"></param>
        public void AddEntity(T item)
        {
            if (Count < EntitiesCollection.Count)
            {
                EntitiesCollection[Count++] = item;
            }
            else
            {
                EntitiesCollection.Add(item);
                Count = EntitiesCollection.Count;
            }
        }

        /// <summary>
        /// Remove entity from the list
        /// </summary>
        /// <param name="idx"></param>
        public void RemoveAt(int idx)
        {
            Debug.Assert(idx < Count);
            Count--;
            var last = EntitiesCollection[Count];
            EntitiesCollection[Count] = null;
            if (idx == Count)
                return;
            EntitiesCollection[idx] = last;
        }

        /// <summary>
        /// Clear all entities
        /// </summary>
        public void Clear()
        {
            for (var i=0; i<Count; i++)
                EntitiesCollection[i] = null;
            Count = 0;
        }

        /// <summary>
        /// Report entities list
        /// </summary>
        /// <returns></returns>
        public string InspectAll()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < Count; i++)
            {
                sb.AppendLine(EntitiesCollection[i].ToString());
            }
            return sb.ToString();
        }
    }
}