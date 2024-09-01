using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class LivesAmountUI : MonoBehaviour
    {
        [SerializeField] private Text livesText;
        [SerializeField] private Player player;

        private void Awake()
        {
            if (player == null) return;
            player.ChangeLivesAmount.AddListener(OnChangeLivesAmount);
            OnChangeLivesAmount();
        }

        private void OnChangeLivesAmount()
        {
            livesText.text = "" + player.NumLives.ToString();
        }

    }
}