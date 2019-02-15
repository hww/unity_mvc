using UnityEngine;
using VARP.MVC;

// NOTE
// This file has a proposal templates of classes from whole object.
// You can kill it and remove all code depended on this classes

namespace VARP.MVC
{
    public enum EventTag
    {
        Null,
        OnAttachTargetConnected,
        OnAttachTargetDisconnected,
        OnAttachHookConnected,
        OnAttachHookDisconnected
    }
}

namespace VARP.ArtPrimitives
{
    public class Zone : MonoBehaviour
    {

    }

    public class Locator : MonoBehaviour
    {
        public Zone zone;
        public Fact[] facts;
    }

    public class Spawner : Locator
    {

    }
}

namespace VARP.FSM
{    
    public class FsmProcess
    {

    }

    public class FsmResource
    {

    }
}

namespace VARP.DataStructures.Interfaces
{
    public interface  IValidate
    {
        void OnValidate();
    }
}
