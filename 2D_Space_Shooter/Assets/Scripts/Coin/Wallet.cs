using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    public class Wallet : MonoBehaviour
    {
        [HideInInspector] public UnityEvent CangeAmountCoins;
        private int coins;
        public int CoinsAmount => coins;

        public void AddCoinsInWallet(int coinsCount)
        {
            if (coinsCount < 0) return;

            coins += coinsCount;

            CangeAmountCoins.Invoke();
        }
    }
}