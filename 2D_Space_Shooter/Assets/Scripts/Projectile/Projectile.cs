using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private ProjectileProperties m_ProjectileProperties;

        private Rigidbody2D m_Rigid;
        private int m_BounceNum;
        private int m_lostDamge;
        private Destructible m_Parent;
        [SerializeField] private UnityEvent m_ImpactEffect;
        [SerializeField] private UnityEvent m_DestroyEffect;


        private void Start()
        {
            Destroy(gameObject, m_ProjectileProperties.Lifetime);

            m_Rigid = GetComponent<Rigidbody2D>();
            m_Rigid.mass = m_ProjectileProperties.Mass;
            m_Rigid.angularDrag = m_ProjectileProperties.AngularDrag;
            m_Rigid.gravityScale = m_ProjectileProperties.GravityScale;

            float stepLenght = Time.deltaTime * m_ProjectileProperties.ThrustForce;
            m_Rigid.AddForce(transform.up * stepLenght, ForceMode2D.Impulse);
        }

        private void Update()
        {
            CheckCollision();
        }

        private void OnDestroy()
        {
            m_DestroyEffect.Invoke();
        }

        private void CheckCollision()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, m_Rigid.velocity.magnitude / m_ProjectileProperties.ThrustForce);

            if (hit.collider)
            {
                Impact(hit.collider.gameObject);
                if (m_ProjectileProperties.CanBounce == true && m_BounceNum < m_ProjectileProperties.MaxBounceNum)
                {
                    m_BounceNum++;
                    m_lostDamge -= m_ProjectileProperties.DamadeLossPerBounce;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }

        private void Impact(GameObject v_object)
        {
            if (v_object == null)
                return;

            var dest = v_object.transform.root.GetComponent<Destructible>();
            if (dest != null && dest != m_Parent)
            {
                dest.GetComponent<SpaceShip>()?.SetLastDamger(m_Parent.gameObject);
                dest.ApplyDamage(m_ProjectileProperties.Damage - m_lostDamge);

                if (m_ProjectileProperties.HasImpactForce)
                    dest.GetComponent<Rigidbody2D>()?.AddForceAtPosition((m_Rigid.mass * m_Rigid.velocity) * m_ProjectileProperties.ImpactForceModifier, transform.position);

                if (m_Parent == Player.Instance.ActiveShip)
                    Player.Instance.AddScore(dest.ScoreValue);
            }

            m_ImpactEffect.Invoke();
        }

        public void SetParentShooter(Destructible parent)
        {
            m_Parent = parent;
        }
    }
}
