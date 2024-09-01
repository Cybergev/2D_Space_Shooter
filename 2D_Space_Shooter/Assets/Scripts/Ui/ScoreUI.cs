using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] private Text scoreText;
        [SerializeField] private Player player;

        private void Awake()
        {
            if (player == null) return;
            player.ChangeScoreAmount.AddListener(OnCangedAmountScore);
            OnCangedAmountScore();
        }

        private void OnCangedAmountScore()
        {
            scoreText.text = "Score: " + player.Score.ToString();
        }
    }
}