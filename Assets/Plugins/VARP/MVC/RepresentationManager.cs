﻿// =============================================================================
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

using System.Text;

namespace VARP.MVC
{
    /// <summary>
    ///     Update all representation with rendering speed.
    /// </summary>
    public static class RepresentationManager
    {
        /// <summary>
        ///     There are all buckets of representations
        /// </summary>
        private static readonly Bucket<Representation>[] Buckets =
            new Bucket<Representation>[(int) BucketTag.BucketsCount];

        static RepresentationManager()
        {
            for (var i = 0; i < (int) BucketTag.BucketsCount; i++)
                Buckets[i] = new Bucket<Representation>();
        }

        /// <summary>
        ///     Function called by the Representation manager to update the state
        ///     of the EntityRepresentation
        /// </summary>
        public static void Update(float realTime, BucketTag bucketTag)
        {
            var bucket = Buckets[(int) bucketTag];
            var i = 0;
            while (i < bucket.Count)
            {
                var entityRepresentation = bucket.EntitiesCollection[i];
                if (entityRepresentation.spawnState == SpawnState.DeSpawn)
                {
                    bucket.RemoveAt(i);
                }
                else
                {
                    entityRepresentation.OnUpdate(realTime);
                    i++;
                }
            }
        }

        /// <summary>
        ///     Add new representation to bucket
        /// </summary>
        /// <param name="representation">Representation</param>
        /// <param name="bucketTag">Bucket tag</param>
        public static void AddRepresentation(Representation representation, BucketTag bucketTag)
        {
            Buckets[(int) bucketTag].AddEntity(representation);
        }

        /// <summary>
        ///     Clear all entities
        /// </summary>
        /// <param name="bucketTag"></param>
        public static void Clear(BucketTag bucketTag)
        {
            Buckets[(int) bucketTag].Clear();
        }

        /// <summary>
        ///     Override this method to deliver messages for representation
        /// </summary>
        /// <param name="evt"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        public static void OnEvent(EventName evt, object arg1 = null, object arg2 = null)
        {
        }

        /// <summary>
        ///     Make report of all buckets
        /// </summary>
        /// <returns></returns>
        public static string InspectAll()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < Buckets.Length; i++)
            {
                sb.AppendLine($"Bucket: {(BucketTag) i}");
                sb.AppendLine(Buckets[i].InspectAll());
            }

            return sb.ToString();
        }
    }
}