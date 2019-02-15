using System;

namespace VARP.MVC.Hooks
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