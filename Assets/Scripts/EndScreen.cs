using QuizMaster.Managers;
using TMPro;
using UnityEngine;

namespace QuizMaster
{
    public class EndScreen : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI finalScoreText;
        private ScoreManager _scoreManager;

        private void Awake()
        {
            _scoreManager = FindObjectOfType<ScoreManager>();
        }

        public void ShowFinalScore()
        {
            finalScoreText.text = "Congratulations!\nYou got a score of " + _scoreManager.CalculateScore() + "%";
        }
    }
}