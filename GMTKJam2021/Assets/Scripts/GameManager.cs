using GMTK2021.Gameplay;
using GMTK2021.Util;
using UnityEngine;

namespace GMTK2021
{
    public class GameManager : MonoBehaviour
    {
        public PlayerBehavior Player { get; private set; }

        [SerializeField]
        private SpawnContainer spawnContainer;

        [SerializeField]
        private UnityScene hudScene;

        private void Start()
        {
            Player = Instantiate(spawnContainer.Player, transform, true);
            Player.transform.position = Vector3.zero;
            Player.GameManager = this;

            hudScene.LoadScene(true);
        }
    }
}