using GMTK2021.Gameplay.Enemy.Base;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK2021.Gameplay
{
    public class EnableEnemies : MonoBehaviour
    {
        [SerializeField]
        private List<EnemyBehavior> enemies;

        public void ActivateAll()
        {
            ChangeStateTo(true);
        }

        private void ChangeStateTo(bool targetState)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] != null)
                {
                    enemies[i].EnemyActive = targetState;
                }
            }
        }

        public void DeactivateAll()
        {
            ChangeStateTo(false);
        }
    }
}
