using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class CoinSpawner : MonoBehaviour
    {
        [SerializeField] private Coin coinPerfab;
        public Coin CoinPerfab => coinPerfab;

        public void SpawnCoin(int v_denomination)
        {
            if (v_denomination <= 0) return;
            Coin coin = Instantiate(CoinPerfab, transform.position, Quaternion.identity);
            coin.transform.position = transform.localPosition + new Vector3(0, 0, 0.05f);
            coin.SetCoinDenomination(v_denomination);
        }

    }

}