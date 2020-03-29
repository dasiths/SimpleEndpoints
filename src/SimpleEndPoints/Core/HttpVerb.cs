using System;

namespace SimpleEndpoints.Core
{
    [Flags]
    public enum HttpVerb
    {
        Get = 1 << 0,
        Post = 1 << 1,
        Put = 1 << 2,
        Delete = 1 << 3,
        Head = 1 << 4,
        Patch = 1 << 5,
        Options = 1 << 6
    }
}