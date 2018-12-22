using System.Collections.Generic;

namespace VARP.MVC
{
    /// <summary>
    /// Update all representation with rendering speed
    /// </summary>
    public class RepresentationManager
    {
        public static readonly List<EntityRepresentation> EntitiesCollection = new List<EntityRepresentation>(100);

        public static int Count;
        
        /// <summary>       
        /// Function called by the Representation manager to update the state
        /// of the EntityRepresentation
        /// </summary>
        public static void Update(float RealTime)
        {
            var j = 0;
            for (int i = 0; i < Count; i++)
            {
                var entityRepresentation = EntitiesCollection[i];
                if (!entityRepresentation.Despawn)
                {
                    entityRepresentation.OnUpdate(RealTime);
                    EntitiesCollection[j++] = entityRepresentation;
                }
            }
            Count = j;
        }

        public static void AddRepresentation(EntityRepresentation entityRepresentation)
        {
            if (Count < EntitiesCollection.Count)
            {
                EntitiesCollection[Count++] = entityRepresentation;
            }
            else
            {
                EntitiesCollection.Add(entityRepresentation);
                Count = EntitiesCollection.Count;
            }
        }

        public static void OnEntityRemoved(Entity entity)
        {
            
        }
        
        public static void OnEntityAdded(Entity entity)
        {
            
        }
    }
}