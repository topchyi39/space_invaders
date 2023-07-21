using System;

namespace ObjectPolling
{
    public interface IPoolObject
    {
        event Action Release;
    }
}