using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    public class Player : SingletonBase<Player>
    {
        [HideInInspector] public UnityEvent ChangeLivesAmount;
        [SerializeField] private UnityEvent m_EventOnShipRespawn;
        [HideInInspector] public UnityEvent EventOnShipRespawn => m_EventOnShipRespawn;
        [SerializeField] private int m_NumLives;
        public int NumLives => m_NumLives;
        [SerializeField] private SpaceShip m_Ship;
        [SerializeField] private GameObject m_PlayerShipPrefab;
        public SpaceShip ActiveShip => m_Ship;
        [SerializeField] private Transform[] m_SpawnPoints;
        private Transform m_SpawnPoint;

        [SerializeField] private CameraController m_CameraController;
        [SerializeField] private MovementController m_MovementController;


        protected override void Awake()
        {
            base.Awake();

            if (m_Ship.TeamId != 0)
            {
                if (m_Ship.TeamId == 1)
                {
                    m_SpawnPoint = m_SpawnPoints[1];
                }
                if (m_Ship.TeamId == 2)
                {
                    m_SpawnPoint = m_SpawnPoints[2];
                }
            }
            else
            {
                m_SpawnPoint = m_SpawnPoints[0];
            }

            if (m_Ship != null) Destroy(m_Ship.gameObject);
        }

        private void Start()
        {
            Respawn();
        }

        private void onShipDeath()
        {
            m_NumLives--;
            ChangeLivesAmount.Invoke();

            m_Ship.EventOnDeath.RemoveListener(onShipDeath);
            if (m_NumLives > 0) Respawn();
            else LevelSequenceController.Instance.FinishCurrentLevel(false);
        }
        
        private void Respawn()
        {
            if(LevelSequenceController.PlayerShip != null)
            {
                var newPlayerShip = Instantiate(LevelSequenceController.PlayerShip, m_SpawnPoint);

                m_Ship = newPlayerShip.GetComponent<SpaceShip>();
                m_Ship.EventOnDeath.AddListener(onShipDeath);
                m_Ship.SetOwner(gameObject);

                m_CameraController.SetTarget(m_Ship.transform);
                m_MovementController.SetTargetShip(m_Ship);
                m_EventOnShipRespawn.Invoke();
            }
        }

        #region Score
        [HideInInspector] public UnityEvent ChangeScoreAmount;
        [HideInInspector] public UnityEvent ChangeKillsAmount;
        public int Score { get; private set; }
        public int NumKills { get; private set; }

        public void AddScore(int num)
        {
            Score += num;
            ChangeScoreAmount.Invoke();
        }
        public void AddKill()
        {
            NumKills++;
            ChangeKillsAmount.Invoke();
        }

        #endregion
    }
}
