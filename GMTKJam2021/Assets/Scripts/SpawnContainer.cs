using GMTK2021.Gameplay;
using UnityEngine;

namespace GMTK2021
{
    [CreateAssetMenu(menuName = "GMTK Jam/Spawn Container")]
    public class SpawnContainer : ScriptableObject
    {
        [SerializeField]
        private PlayerBehavior player;

        public PlayerBehavior Player => player;
    }
}