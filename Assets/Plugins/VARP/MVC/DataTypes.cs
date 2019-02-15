namespace VARP.MVC
{
    
    /// <summary>
    ///     Available buckets
    /// </summary>
    public enum BucketTag
    {
        Camera,
        Players,
        Defaults,
        BucketsCount
    }
    
    /// <summary>
    ///     Spawning state
    /// </summary>
    public enum SpawnState
    {
        None,
        Ready,
        DeSpawn
    }

    /// <summary>
    ///     Result of methods
    /// </summary>
    public enum Result
    {
        Null,
        Success,
        InvalidReturn,
    }
    
    /// <summary>
    ///     Each fact is key value pair
    /// </summary>
    public struct Fact
    {
        public string key;
        public string value;
    }
}