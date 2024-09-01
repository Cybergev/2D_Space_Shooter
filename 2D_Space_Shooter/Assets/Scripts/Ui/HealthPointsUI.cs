using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class HealthPointsUI : MonoBehaviour
    {
        [SerializeField] private Text HealthText;
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
            ship.ChangeHitPoints.AddListener(OnChangeHitPoints);
            ship.EventOnDeath.AddListener(RefindSpip);
            OnChangeHitPoints();
        }
        private void OnChangeHitPoints()
        {
            HealthText.text = "" + ship.HitPoints.ToString();
        }

    }
}