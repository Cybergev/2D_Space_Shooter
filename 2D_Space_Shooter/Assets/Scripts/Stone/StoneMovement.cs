using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class StoneMovement : MonoBehaviour
    {
        /// <summary>
        ///Масса для автоматической установки у ригида.
        /// </summary>
        [SerializeField] private float m_Mass;

        /// <summary>
        /// Максимальная лмнейная скорость.
        /// </summary>
        [SerializeField] private float m_MaxLinearVelocity;

        /// <summary>
        /// Сохраненная ссылка на ригид.
        /// </summary>
        private Rigidbody2D m_Rigid;

        /// <summary>
        /// Определяет направление и скорость движения в нем.
        /// </summary>
        [SerializeField] private Vector3 m_Direction;
        /// <summary>
        /// Определяет ускорение.
        /// </summary>
        [SerializeField] private float velocity;

        private void Start()
        {
            m_Rigid = GetComponent<Rigidbody2D>();
            m_Rigid.mass = m_Mass;

            m_Rigid.inertia = 1;
            Move();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            float stepLenght = Time.fixedDeltaTime * velocity * m_MaxLinearVelocity;
            Vector3 step = m_Direction * stepLenght;
            m_Rigid.AddForce(step);
        }

        public void ChangeDirection(Vector3 v_directionValue)
        {
            if (v_directionValue == Vector3.zero) return;
            m_Direction = v_directionValue;
        }

        public void ChangeVelocity(float v_velocityValue)
        {
            if (v_velocityValue <= 0) return;
            velocity = v_velocityValue;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            m_Direction = new Vector3();
        }
    }
}
