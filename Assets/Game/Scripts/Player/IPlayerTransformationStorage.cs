using UnityEngine;

namespace Game.Scripts.Player
{
    public interface IPlayerTransformationStorage
    {
        Vector3 PlayerPosition { get; }
    }
}