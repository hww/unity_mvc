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

using System.Text;

namespace VARP.MVC
{
    /// <summary>
    ///     Update all models with physics speed
    /// </summary>
    public static class EntityManager 
    {
        public static readonly Bucket<Entity>[] Buckets = new Bucket<Entity>[(int)BucketTag.BucketsCount];

        static EntityManager()
        {
            for (var i=0; i<(int)BucketTag.BucketsCount; i++)
                Buckets[i] = new Bucket<Entity>();
        }

        /// <summary>
        ///     Update single bucket
        /// </summary>
        /// <param name="fixedFrequencyTime"></param>
        /// <param name="bucketTag"></param>
        public static void Update(float fixedFrequencyTime, BucketTag bucketTag)
        {
            var bucket = Buckets[(int) bucketTag];
            var i = 0;
            while (i < bucket.Count)
            {
                var entity = bucket.EntitiesCollection[i];
                if (entity.spawnState == SpawnState.DeSpawn)
                {
                    RepresentationManager.OnEvent(EventName.EntityDisabled, entity);
                    bucket.RemoveAt(i);
                }
                else
                {
                    entity.PreUpdate(fixedFrequencyTime);
                    entity.OnUpdate(fixedFrequencyTime);
                    i++;
                }
            }
        }
        
        /// <summary>
        ///     Add single entity to the list
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="bucketTag"></param>
        public static void AddEntity(Entity entity, BucketTag bucketTag)
        {
            Buckets[(int)bucketTag].AddEntity(entity);
            RepresentationManager.OnEvent(EventName.EntityEnabled, entity);
        }

        /// <summary>
        ///     Clear all entities
        /// </summary>
        /// <param name="bucketTag"></param>
        public static void Clear(BucketTag bucketTag)
        {
            Buckets[(int)bucketTag].Clear();
        }

        /// <summary>
        ///     Make string with report about all buckets
        /// </summary>
        /// <returns></returns>
        public static string InspectAll()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < Buckets.Length; i++)
            {
                sb.AppendLine($"Bucket: {((BucketTag)i)}");
                sb.AppendLine(Buckets[i].InspectAll());
            }
            return sb.ToString();
        }
    }
}