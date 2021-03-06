﻿using System.Collections.Generic;
using Plugins.VARP.MVC;
using UnityEngine.Experimental;

namespace VARP.MVC
{
    /// <summary>
    /// Update all models with physics speed
    /// </summary>
    public class EntryManager 
    {
        public static readonly List<Entity> EntitiesCollection = new List<Entity>(100);
        public static int Count;
        
        public static void Update(float FixedFrequencyTime)
        {
            var j = 0;
            for (int i = 0; i < Count; i++)
            {
                var entity = EntitiesCollection[i];
                if (entity.Despawn)
                {
                    RepresentationManager.OnEvent(EventName.EntityDisabled, entity);
                }
                else
                {
                    entity.PreUpdate(FixedFrequencyTime);
                    entity.OnUpdate(FixedFrequencyTime);
                    EntitiesCollection[j++] = entity;
                }
            }
            Count = j;
        }
        
        public static void AddEntity(Entity entity)
        {
            if (Count < EntitiesCollection.Count)
            {
                EntitiesCollection[Count++] = entity;
            }
            else
            {
                EntitiesCollection.Add(entity);
                Count = EntitiesCollection.Count;
            }
            RepresentationManager.OnEvent(EventName.EntityEnabled, entity);
        }
    }
}