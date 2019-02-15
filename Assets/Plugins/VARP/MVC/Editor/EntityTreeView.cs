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
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VARP.MVC
{
    class EntityTreeView : TreeView
    {
        public EntityTreeView(TreeViewState treeViewState)
            : base(treeViewState)
        {
            Reload();
        }
        
        protected override TreeViewItem BuildRoot ()
        {
            var root = new TreeViewItem      { id = 0, depth = -1, displayName = "Scene" };
            var buckets = EntityManager.Buckets;
            var bucketTrees = new TreeViewItem[buckets.Length];
            for (var i = 0; i < buckets.Length; i++)
            {
                var bucket = buckets[i];
                if (bucket != null)
                {
                    var bucketTree = BuildTree(bucket,  (BucketTag)i);
                    bucketTrees[i] = bucketTree;
                    root.AddChild(bucketTree);
                }
            }
            SetupDepthsFromParentsAndChildren(root);
            return root;
        }

        
        TreeViewItem BuildTree(Bucket<Entity> bucket, BucketTag bucketTag)
        {
            var bucketTree = new TreeViewItem { id = 0, displayName = bucketTag.ToString() };
            for (var i = 0; i < bucket.Count; i++)
            {
                var entity = bucket.GetAt(i);
                if (entity != null)
                {
                    var entityTree = BuildTree(entity);
                    bucketTree.AddChild(entityTree);
                }
            }
            return bucketTree;
        }
        
        TreeViewItem BuildTree(Entity parent)
        {
            
            var entityTree = new TreeViewItem { id = parent.gameObject.GetInstanceID(), displayName = parent.name };
            var representation = parent.representation;
            if (representation != null)
            {
                var representTree = new TreeViewItem { id = representation.gameObject.GetInstanceID(), displayName = representation.name };
                entityTree.AddChild(representTree);
            }
            var process = parent.process;
            if (process != null)
            {
                var representTree = new TreeViewItem { id = 0, displayName = process.ToString() };
                entityTree.AddChild(representTree);
                
            }
            if (process != null)
            {
                var representTree = new TreeViewItem { id = -1, displayName = process.ToString() };
                entityTree.AddChild(representTree);
            }
            return entityTree;
        }
        
        // Selection
        protected override void SelectionChanged (IList<int> selectedIds)
        {
            Selection.instanceIDs = selectedIds.ToArray();
        }

    }
}