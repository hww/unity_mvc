using System;

namespace VARP.MVC.Connectors
{
    /// <summary>
    ///     Options of attaching methods
    /// </summary>
    [Flags]
    public enum AttachOptions
    {
        Default,
        Move,
        Rotate
    }
}