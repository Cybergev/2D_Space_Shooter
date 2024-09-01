using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [System.Serializable]
    public class StoneColors
    {
        public Color Colors;
    }
    public class StoneColor : MonoBehaviour
    {
        [SerializeField] private StoneColors[] colors;
        private SpriteRenderer stoneRenderer;
        private void Start()
        {
            stoneRenderer = GetComponentInChildren<SpriteRenderer>();
            stoneRenderer.color = colors[Random.Range(0, colors.Length)].Colors;
        }
    }
}
