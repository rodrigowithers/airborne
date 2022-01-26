using UnityEngine;

namespace Game.Scripts.Player
{
    public interface IPlayerTransformationStorage
    {
        Transform GetPlayerTransform();
        Vector3 GetPlayerPosition();
    }
}