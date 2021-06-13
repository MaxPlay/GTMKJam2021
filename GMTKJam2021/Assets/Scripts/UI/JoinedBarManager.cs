using UnityEngine;

namespace GMTK2021.UI
{
    public class JoinedBarManager : MonoBehaviour
    {
        [SerializeField]
        private DelayedBar healthBar;

        [SerializeField]
        private DelayedBar fuelBar;

        [SerializeField]
        private GameManager gameManager;

        private void Start()
        {
            Debug.Assert(gameManager);
        }

        private void Update()
        {
            healthBar.SetValue(gameManager.Player.Health);
            healthBar.SetMaxValue(gameManager.Player.MaxHealth);
            fuelBar.SetValue(gameManager.Player.Fuel);
            fuelBar.SetMaxValue(gameManager.Player.MaxFuel);
        }
    }
}
