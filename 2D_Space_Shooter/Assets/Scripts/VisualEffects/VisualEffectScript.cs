using UnityEngine;

namespace SpaceShooter
{
    public class VisualEffectScript : MonoBehaviour
    {
        [SerializeField] public GameObject ObjectPrefab;
        [SerializeField] public Transform EffectBaseScale;
        /// <summary>
        /// ���������� ���������� �������� ���������. ���� �������� 0 ��� ������ ����� ����������� ��� 1.
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
