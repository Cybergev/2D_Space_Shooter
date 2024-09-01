using UnityEngine;

namespace SpaceShooter
{
    public class ExplosionEffect : MonoBehaviour
    {
        [SerializeField] private float ExplosionlifeTime;

        private void Start()
        {
            Destroy(gameObject, ExplosionlifeTime);
        }

    }
}
