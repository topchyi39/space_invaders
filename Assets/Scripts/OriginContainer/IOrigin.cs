using UnityEngine;

namespace OriginContainer
{
    public interface IOrigin
    {
        Vector3 Up { get; }
        Vector3 Down { get; }
        Vector3 Left { get; }
        Vector3 Right { get; }
        Transform Transform { get; }
    }
}