using UnityEngine;

namespace SpaceShooter
{
    public class MovementController : MonoBehaviour
    {
        public enum ControlMode
        {
            Keyboard,
            Mobile
        }

        [SerializeField] private ControlMode m_ControlMode;
        public void SetTargetShip(SpaceShip ship) => m_TargetShip = ship;

        [SerializeField] private SpaceShip m_TargetShip;

        [SerializeField] private VirtualJoystick m_MobileJoystick;

        [SerializeField] private PointerClickHold m_MobileFirePrimary;
        [SerializeField] private PointerClickHold m_MobileFireSecondary;

        private void Start()
        {
            if (m_ControlMode == ControlMode.Keyboard)
            {
                m_MobileJoystick.gameObject.SetActive(false);
                m_MobileFirePrimary.gameObject.SetActive(false);
                m_MobileFireSecondary.gameObject.SetActive(false);
            }
            if (m_ControlMode == ControlMode.Mobile)
            {
                m_MobileJoystick.gameObject.SetActive(true);
                m_MobileFirePrimary.gameObject.SetActive(true);
                m_MobileFireSecondary.gameObject.SetActive(true);
            }
        }

        private void Update()
        {
            if (m_TargetShip == null) return;
            if (m_ControlMode == ControlMode.Keyboard) ControlKeyboard();
            if (m_ControlMode == ControlMode.Mobile) ControlMobile();
        }

        private void ControlKeyboard()
        {
            float thrust = 0;
            float torque = 0;

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) thrust = 1.0f;
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) thrust = -1.0f;
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) torque = 1.0f;
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) torque = -1.0f;

            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) m_TargetShip.Fire(TurretMode.Primary);
            if (Input.GetKey(KeyCode.LeftAlt) || Input.GetMouseButton(1)) m_TargetShip.Fire(TurretMode.Secondary);

            m_TargetShip.ThrustControl = thrust;
            m_TargetShip.TorqueControl = torque;

        }
        private void ControlMobile()
        {
            Vector3 dir = m_MobileJoystick.Value;

            var dot = Vector2.Dot(dir, m_TargetShip.transform.up);
            var dot2 = Vector2.Dot(dir, m_TargetShip.transform.right);

            if (m_MobileFirePrimary.IsHold == true) m_TargetShip.Fire(TurretMode.Primary);
            if (m_MobileFireSecondary.IsHold == true) m_TargetShip.Fire(TurretMode.Secondary);

            m_TargetShip.ThrustControl = Mathf.Max(0, dot);
            m_TargetShip.TorqueControl = -dot2;
        }

    }

}