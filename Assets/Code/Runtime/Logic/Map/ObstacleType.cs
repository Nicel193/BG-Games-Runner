using System;

namespace Code.Runtime.Logic.Map
{
    [Flags]
    public enum ObstacleType
    {
        None = 0,
        Urban = 1 << 0,
        Suburban = 1 << 1,
        Industrial = 1 << 2,
    }
}