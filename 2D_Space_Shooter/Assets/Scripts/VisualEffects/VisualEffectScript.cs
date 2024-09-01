using UnityEngine;

namespace SpaceShooter
{
    public class VisualEffectScript : MonoBehaviour
    {
        [SerializeField] public GameObject ObjectPrefab;
        [SerializeField] public Transform EffectBaseScale;
        /// <summary>
        /// Переменная умножающая конечный результат. Если значение 0 или меньше будет установлено как 1.
        /// </summary>
        [SerializeField] public float EffectScaleModifier;
        [SerializeField] public Transform EffectSpawnTarget;

        public void SpawnEffect()
        {
            if (EffectScaleModifier <= 0) EffectScaleModifier = 1f;
            GameObject gameObject = Instantiate(ObjectPrefab, EffectSpawnTarget.position, EffectSpawnTarget.rotation);
            gameObject.transform.localScale = EffectBaseScale.localScale * EffectScaleModifier;

        }

    }
}
