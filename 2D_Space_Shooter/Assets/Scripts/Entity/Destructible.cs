using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace SpaceShooter
{
    /// <summary>
    /// ������ ������� ���������.
    /// </summary>
    public class Destructible : Entity
    {
        #region Properties
        [HideInInspector] public UnityEvent ChangeHitPoints;
        /// <summary>
        /// ������ ���������� �����������.
        /// </summary>
        [SerializeField] private bool m_Indestructible;
        public bool IsIndestuctible => m_Indestructible;

        /// <summary>
        /// ������� ���������� ����������.
        /// </summary>
        [SerializeField] protected int m_HitPoints;

        /// <summary>
        /// ������� ���������.
        /// </summary>
        private int m_CurrentHitPoints;
        public int HitPoints => m_CurrentHitPoints;
        public void SetMaxHitPoints(int v_hitPoints)
        {
            if (v_hitPoints <= 0) return;
            m_HitPoints = v_hitPoints;
        }

        #endregion

        #region Unity Events
        /// <summary>
        /// ���������� ���������� ����������.
        /// </summary>
        protected virtual void Start()
        {
            m_CurrentHitPoints = m_HitPoints;
            ChangeHitPoints.Invoke();
        }

        /// <summary>
        /// ���������������� ������� ����������� �������, ����� ��������� ���� ��� ����� ����.
        /// </summary>
        protected virtual void OnDeath()
        {
            m_EventOnDeath?.Invoke();
            Destroy(gameObject);
        }

        private static HashSet<Destructible> m_AllDestructibles;

        public static IReadOnlyCollection<Destructible> AllDestructibles => m_AllDestructibles;

        protected virtual void OnEnable()
        {
            if (m_AllDestructibles == null) m_AllDestructibles = new HashSet<Destructible>();

            m_AllDestructibles.Add(this);
        }

        protected virtual void OnDestroy()
        {
            m_AllDestructibles.Remove(this);
        }

        [SerializeField] private UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;

        #endregion

        #region Teams
        public const int TeamIdNeutral = 0;

        [SerializeField] private int m_TeamId;
        public int TeamId => m_TeamId;

        #endregion

        #region Score

        [SerializeField] private int m_ScoreValue;
        public int ScoreValue => m_ScoreValue;

        #endregion

        #region Public API
        /// <summary>
        /// ���������� ����� � �������.
        /// </summary>
        /// <param name="damage"> ���� ��������� �������.</param>
        public void ApplyDamage(int damage)
        {
            if (m_Indestructible) return;

            m_CurrentHitPoints -= damage;

            if (m_CurrentHitPoints <= 0)
            {
                m_CurrentHitPoints = 0;
                OnDeath();
            }

            ChangeHitPoints.Invoke();

        }
        #endregion

    }
}
