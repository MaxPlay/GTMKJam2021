using GMTK2021.Gameplay.Enemy;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GMTK2021.UI
{
    public class JoinedBarManager : MonoBehaviour
    {
        [SerializeField]
        private DelayedBar healthBar;

        [SerializeField]
        private DelayedBar fuelBar;

        private GameManager gameManager;

        [SerializeField]
        GameObject YouDiedText;

        [SerializeField]
        GameObject VictoryText;

        private BuggyBehavior boss;

        float deathCooldown = 5;
        float deathTimer = -999;

        bool hasBoss = false;

        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            Debug.Assert(gameManager);
            BuggyBehavior[] enemies = FindObjectsOfType<BuggyBehavior>();
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].IsBossEnemy)
                {
                    boss = enemies[i];
                    hasBoss = true;
                    break;
                }
            }
        }

        private void Update()
        {
            healthBar.SetValue(gameManager.Player.Health);
            healthBar.SetMaxValue(gameManager.Player.MaxHealth);
            fuelBar.SetValue(gameManager.Player.Fuel);
            fuelBar.SetMaxValue(gameManager.Player.MaxFuel);
            if (gameManager.Player.Health <= 0)
            {
                YouDiedText.gameObject.SetActive(true);
                if (deathTimer <= 0)
                    deathTimer = Time.time;
                if (Time.time > deathTimer + deathCooldown)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
            if(hasBoss && !boss)
            {
                VictoryText.SetActive(true);
            }
        }
    }
}
