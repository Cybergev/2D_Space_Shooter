using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CollisionDamageApplicator : MonoBehaviour
    {
        [Header("Урон по цели при столкновении с ней")]
        public static string IgnoreTag = "WorldBoundary";
        [SerializeField] private float m_VelocityDamageModifier;
        [SerializeField] private float m_DamageConstante;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag == IgnoreTag) return;

            Destructible destructable = collision.transform.root.GetComponent<Destructible>();

            if (destructable != null)
            {
                destructable.ApplyDamage((int)m_DamageConstante + (int)(m_VelocityDamageModifier * (collision.rigidbody.mass * collision.relativeVelocity.magnitude)));
            }
        }
    }
}
