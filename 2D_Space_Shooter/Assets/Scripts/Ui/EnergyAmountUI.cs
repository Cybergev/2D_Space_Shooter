using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class EnergyAmountUI : MonoBehaviour
    {
        [SerializeField] private Text EnergyText;
        [SerializeField] private Player player;
        private SpaceShip ship;

        private void Update()
        {
            if (ship != null) return;
            RefindSpip();
        }

        private void RefindSpip()
        {
            if (player == null) return;
            ship = player.ActiveShip;
            ship.ChangeEnergyAmount.AddListener(OnChangeEnergyAmount);
            ship.EventOnDeath.AddListener(RefindSpip);
            OnChangeEnergyAmount();
        }
        private void OnChangeEnergyAmount()
        {
            EnergyText.text = "" + ship.PrimaryEnergy.ToString();
        }

    }
}