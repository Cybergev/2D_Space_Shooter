using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class StatisticPanel : MonoBehaviour
    {
        [SerializeField] private Text m_Kills;
        [SerializeField] private Text m_Score;
        [SerializeField] private Text m_Time;

        private void Awake()
        {
            m_Kills.text = "Max kills : " + PlayerPrefs.GetInt("SpaceShooter:MaxKills").ToString();
            m_Score.text = "Max score : " + PlayerPrefs.GetInt("SpaceShooter:MaxScore").ToString();
            m_Time.text = "Min time : " + PlayerPrefs.GetInt("SpaceShooter:MinTime").ToString();
        }

    }
}