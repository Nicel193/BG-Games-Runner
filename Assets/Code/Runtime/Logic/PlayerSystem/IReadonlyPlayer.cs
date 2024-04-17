using UnityEngine;

namespace Code.Runtime.Logic
{
    public interface IReadonlyPlayer
    {
        Rigidbody Rigidbody { get; }
        BoxCollider BoxCollider { get; }
    }
}