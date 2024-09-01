using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class CoinsUI : MonoBehaviour
    {
        [SerializeField] private Text coinText;
        [SerializeField] private Wallet wallet;

        private void Awake()
        {
            if (wallet == null) return;
            wallet.CangeAmountCoins.AddListener(OnCangedAmountCoins);
            OnCangedAmountCoins();
        }

        private void OnCangedAmountCoins()
        {
            coinText.text = "Coins: " + wallet.CoinsAmount.ToString();
        }
    }
}