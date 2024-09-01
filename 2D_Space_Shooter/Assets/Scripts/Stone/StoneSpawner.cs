using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    public class StoneSpawner : MonoBehaviour
    {

        [Header("Spawn")]
        [SerializeField] private Stone stonePrefab;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private Transform spawnDirection;
        [SerializeField] private float stoneSpeed;
        [SerializeField] private float spawnRate;

        [Header("Balance")]
        [SerializeField] private int amount;
        [SerializeField] [Range(0.0f, 1.0f)] private float minHitpointsPercentage;
        [SerializeField] private float maxHitpointsRate;

        [Space(10)] public UnityEvent Comepleted;

        private float timer;
        private int amountSpawned;
        private int stoneMaxHitpoints;
        private int stoneMinHitpoints;


        private void Start()
        {
            timer = spawnRate;
        }

        private void Update()
        {

            timer += Time.deltaTime;

            if (timer >= spawnRate)
            {
                Spawn();

                timer = 0;
            }
            if (amountSpawned == amount)
            {
                enabled = false;

                Comepleted.Invoke();
            }
        }
        public void SetSpawnAmount(int vaule)
        {
            if (vaule < 0) return;
            amount = vaule;
            amountSpawned = vaule;
        }


        public void Spawn()
        {
            StoneMovement stoneMovement;
            Stone stone = Instantiate(stonePrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
            stone.SetSize((Stone.Sizes)Random.Range(1, 4));
            stone.SetMaxHitPoints(Random.Range(stoneMinHitpoints, stoneMaxHitpoints + 1) * ((int)stone.Size + 1));

            stoneMovement = stone.transform.GetComponent<StoneMovement>();
            stoneMovement.ChangeDirection((spawnDirection.transform.position - stone.transform.position));
            stoneMovement.ChangeVelocity(stoneSpeed);

            amountSpawned++;
        }
    }
}
