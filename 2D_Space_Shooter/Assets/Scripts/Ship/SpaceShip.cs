using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {
        [Header("Space ship")]
        private GameObject m_Owner;
        public GameObject Owner => m_Owner;
        [SerializeField] private Sprite m_PreviewImage;
        public Sprite PreviewImage => m_PreviewImage;
        /// <summary>
        ///Масса для автоматической установки у ригида.
        /// </summary>
        [SerializeField] private float m_Mass;
        /// <summary>
        /// Тяга.
        /// </summary>
        [SerializeField] private float m_Thrust;
        /// <summary>
        /// Вращающая сила.
        /// </summary>
        [SerializeField] private float m_Mobility;
        /// <summary>
        /// Максимальная лмнейная скорость.
        /// </summary>
        [SerializeField] private float m_MaxLinearVelocity;
        public float MaxLinearVelocity => m_MaxLinearVelocity;
        /// <summary>
        /// Максимальная вращательная скорость. В градус/сек
        /// </summary>
        [SerializeField] private float m_MaxAngularVelocity;
        public float MaxAngularVelocity => m_MaxAngularVelocity;
        /// <summary>
        /// Сохраненная ссылка на ригид.
        /// </summary>
        private Rigidbody2D m_Rigid;

        private GameObject m_LastDamger;
        public GameObject LastDamger => m_LastDamger;

        #region Public API
        public void SetOwner(GameObject v_gameObject)
        {
            if(v_gameObject != null)
            m_Owner = v_gameObject;
        }
        public void SetLastDamger(GameObject v_gameObject)
        {
            if (v_gameObject != null)
            m_LastDamger = v_gameObject;
        }
        /// <summary>
        /// Управление линейной тягой. -1.0 до +1.0
        /// </summary>
        public float ThrustControl { get; set; }

        /// <summary>
        /// Управление вращательной тягой. -1.0 до +1.0
        /// </summary>
        public float TorqueControl { get; set; }
        #endregion

        #region Unity Events
        protected override void Start()
        {
            base.Start();

            m_Rigid = GetComponent<Rigidbody2D>();
            m_Rigid.mass = m_Mass;

            m_Rigid.inertia = 1;

            InitOffensive();
        }

        private void FixedUpdate()
        {
            UpdateRigitBody();
            UpdateEnergyRegen();
            if (m_PastEnergyAmount != m_PrimaryEnergy)
            {
                m_PastEnergyAmount = m_PrimaryEnergy;
                ChangeEnergyAmount.Invoke();
            }
            if (m_PastSecondaryAmmoAmount != m_SecondaryAmmo)
            {
                m_PastSecondaryAmmoAmount = m_SecondaryAmmo;
                ChangeAmmoAmount.Invoke();
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (m_LastDamger != null)
            {
                m_LastDamger.transform.root.GetComponent<SpaceShip>()?.Owner.transform.root.GetComponent<Player>()?.AddKill();
            }
        }
        #endregion

        /// <summary>
        /// Метод добавления сил кораблю для движения.
        /// </summary>
        private void UpdateRigitBody()
        {
            m_Rigid.AddForce(ThrustControl * m_Thrust * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddForce(-m_Rigid.velocity * (m_Thrust / m_MaxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddTorque(TorqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddTorque(-m_Rigid.angularVelocity * (m_Mobility / m_MaxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
        }



        [SerializeField] private Turret[] m_Turrets;

        [SerializeField] private int m_MaxEnergy;
        [SerializeField] private int m_EnergyRegenPerSecond;

        [SerializeField] private int m_MaxAmmo;

        private float m_PastEnergyAmount;
        private int m_PastSecondaryAmmoAmount;

        private float m_PrimaryEnergy;
        private int m_SecondaryAmmo;

        public float PrimaryEnergy => m_PrimaryEnergy;
        public float SecondaryAmmo => m_SecondaryAmmo;

        public void Fire(TurretMode mode)
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                if (m_Turrets[i].Mode == mode)
                {
                    m_Turrets[i].Fire();
                }
            }
        }

        public void InitOffensive()
        {
            m_PrimaryEnergy = m_MaxEnergy;
            m_SecondaryAmmo = m_MaxAmmo;
        }

        private void UpdateEnergyRegen()
        {
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + (float)m_EnergyRegenPerSecond * Time.fixedDeltaTime, 0, m_MaxEnergy);
        }

        public void AddEnergy(int energy)
        {
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + energy, 0, m_MaxEnergy);
        }
        public bool DrawEnergy(int count)
        {
            if (count == 0) return true;
            if (m_PrimaryEnergy >= count)
            {
                m_PrimaryEnergy -= count;
                return true;
            }
            return false;
        }

        public void AddAmmo(int ammo)
        {
            m_SecondaryAmmo = Mathf.Clamp(m_SecondaryAmmo + ammo, 0, m_MaxAmmo);
        }

        public bool DrawAmmo (int count)
        {
            if (count == 0) return true;
            if (m_SecondaryAmmo >= count)
            {
                m_SecondaryAmmo -= count;
                return true;
            }
            return false;
        }

        [HideInInspector] public UnityEvent ChangeEnergyAmount;
        [HideInInspector] public UnityEvent ChangeAmmoAmount;

        public void AssignWeapon(TuerretProperties props)
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                m_Turrets[i].AssignLoadut(props);
            }
        }
    }
}
