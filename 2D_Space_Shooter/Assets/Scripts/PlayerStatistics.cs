using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class PlayerStatistics : MonoBehaviour
    {
        public int numKills;
        public int score;
        public int time;

        public void Reset()
        {
            numKills = 0;
            score = 0;
            time = 0;
        }
        public void CalculateLevelStatistic()
        {
            numKills = Player.Instance.NumKills;
            score = Player.Instance.Score;
            time = (int)LevelController.Instance.LevelTime;
        }
        public void SaveLevelStatistic()
        {
            if (PlayerPrefs.GetInt("SpaceShooter:MaxKills") < numKills)
            {
                PlayerPrefs.SetInt("SpaceShooter:MaxKills", numKills);
            }
            if (PlayerPrefs.GetInt("SpaceShooter:MaxScore") < score)
            {
                PlayerPrefs.SetInt("SpaceShooter:MaxScore", score);
            }
            if (PlayerPrefs.GetInt("SpaceShooter:MinTime") > time || PlayerPrefs.GetInt("SpaceShooter:MinTime") == 0)
            {
                PlayerPrefs.SetInt("SpaceShooter:MinTime", time);
            }
        }
    }
}