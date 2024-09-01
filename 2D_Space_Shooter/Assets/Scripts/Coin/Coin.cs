using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private int denomination;
        [SerializeField] private Text coinDenominationText;
        private Wallet m_Wallet;

        private void Start()
        {
            m_Wallet = FindObjectOfType<Wallet>();
            coinDenominationText.text = denomination.ToString();
        }
        public void SetCoinDenomination(int vaule)
        {
            if (vaule < 0) return;
            denomination += vaule;
            coinDenominationText.text = denomination.ToString();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            SpaceShip ship = collision.transform.root.GetComponent<SpaceShip>();

            if (ship != null)
            {
                m_Wallet.AddCoinsInWallet(denomination);
                Destroy(gameObject);
            }
        }
    }
}