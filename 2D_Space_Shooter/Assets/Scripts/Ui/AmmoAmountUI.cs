using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class AmmoAmountUI : MonoBehaviour
    {
        [SerializeField] private Text AmmoText;
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
            ship.ChangeAmmoAmount.AddListener(OnChangeAmmoAmount);
            ship.EventOnDeath.AddListener(RefindSpip);
            OnChangeAmmoAmount();
        }
        private void OnChangeAmmoAmount()
        {
            AmmoText.text = "" + ship.SecondaryAmmo.ToString();
        }

    }
}