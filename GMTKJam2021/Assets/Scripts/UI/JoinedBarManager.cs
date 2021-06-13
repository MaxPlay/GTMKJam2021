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

        float deathCooldown = 5;
        float deathTimer = -999;

        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            Debug.Assert(gameManager);
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
        }
    }
}
