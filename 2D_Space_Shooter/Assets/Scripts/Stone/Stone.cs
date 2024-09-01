using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Stone : Destructible
    {
        public enum Sizes
        {
            Small,
            Normal,
            Big,
            Huge
        }

        [SerializeField] private Sizes size;
        private Rigidbody2D m_Rigit;
        private SpriteRenderer stoneRenderer;
        private Color stoneColor;

        public Sizes Size => size;

        private void Awake()
        {
            EventOnDeath.AddListener(OnStoneDestroyed);
            SetSize(size);
            SetMaxHitPoints((((int)size) + 1) * 100);
            m_Rigit = GetComponent<Rigidbody2D>();
            m_Rigit.mass = m_HitPoints / 2;

            stoneColor = new Color(Random.Range(100, 200), Random.Range(100, 200), Random.Range(100, 200), 255);

            stoneRenderer = GetComponentInChildren<SpriteRenderer>();
            stoneRenderer.color = stoneColor;
        }

        public void OnStoneDestroyed()
        {
            if (size != Sizes.Small)
            {
                DivideStone();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void DivideStone()
        {
            for (int i = 0; i < 2; i++)
            {
                transform.position = transform.localPosition;
                Stone stone = Instantiate(this, transform.position, Quaternion.identity);
                stone.SetSize(size - 1);
                stone.SetMaxHitPoints(Mathf.Clamp(m_HitPoints / 2, 1, m_HitPoints));
                stone.m_Rigit.mass = m_HitPoints / 2;

                SpriteRenderer stoneSprite = stone.GetComponentInChildren<SpriteRenderer>();
                stoneSprite.color = stoneColor;
            }
            Destroy(gameObject);
        }

        public void SetSize(Sizes size)
        {
            if (size < 0) return;

            transform.localScale = GetVectorFormSize(size);
            this.size = size;
        }

        private Vector3 GetVectorFormSize(Sizes size)
        {
            if (size == Sizes.Small) return new Vector3(0.4f, 0.4f, 0.4f);
            if (size == Sizes.Normal) return new Vector3(0.6f, 0.6f, 0.6f);
            if (size == Sizes.Big) return new Vector3(0.75f, 0.75f, 0.75f);
            if (size == Sizes.Huge) return new Vector3(1, 1, 1);

            return Vector3.one;
        }

    }

}