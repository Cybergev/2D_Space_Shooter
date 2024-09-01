using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class PowerupEnergyShield : MonoBehaviour
    {
        [SerializeField] private GameObject shielPrefab;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            SpaceShip ship = collision.transform.root.GetComponent<SpaceShip>();

            if (ship != null)
            {
                GameObject shield = Instantiate(shielPrefab, ship.transform.position, Quaternion.identity);
                ship.transform.SetParent(shield.transform.root);
                Destroy(gameObject);
            }
        }
    }
}