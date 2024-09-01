using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    [RequireComponent(typeof(Destructible))]
    public class StoneHitpointsText : MonoBehaviour
    {
        [SerializeField] private Text hitpointText;

        private Destructible destructible;

        private void Awake()
        {
            destructible = GetComponent<Destructible>();

            destructible.ChangeHitPoints.AddListener(OnChangeHitPoint);
        }

        private void OnDestroy()
        {
            destructible.ChangeHitPoints.RemoveListener(OnChangeHitPoint);
        }

        private void OnChangeHitPoint()
        {
            int hitPoints = destructible.HitPoints;

            if (hitPoints >= 1000) hitpointText.text = hitPoints / 1000 + "k";
            else hitpointText.text = hitPoints.ToString();
        }
    }

}