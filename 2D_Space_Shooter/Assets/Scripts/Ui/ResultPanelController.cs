using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class ResultPanelController : SingletonBase<ResultPanelController>
    {
        [SerializeField] private Text m_Kills;
        [SerializeField] private Text m_Score;
        [SerializeField] private Text m_Time;

        [SerializeField] private Text m_Result;
        [SerializeField] private Text m_ButtonNextText;

        private bool m_Success;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void ShownResults(PlayerStatistics levelResults, bool success)
        {
            gameObject.SetActive(true);

            m_Success = success;

            m_Result.text = success ? "Win" : "Lose";

            m_Kills.text = "Kills : " + levelResults.numKills.ToString();
            m_Score.text = "Score : " + levelResults.score.ToString();
            m_Time.text = "Time : " + levelResults.time.ToString();

            m_ButtonNextText.text = success ? "Next" : "Restart";

            Time.timeScale = 0;
        }

        public void OnButtonNextAction()
        {
            gameObject.SetActive(false);

            Time.timeScale = 1;

            if (m_Success)
            {
                LevelSequenceController.Instance.AvanceLevel();
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
