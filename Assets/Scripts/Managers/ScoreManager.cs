using UnityEngine;

namespace QuizMaster.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        private int _correctAnswers;
        private int _questionsSeen;

        public int GetCorrectAnswers()
        {
            return _correctAnswers;
        }

        public void IncrementCorrectAnswers()
        {
            _correctAnswers++;
        }

        public int GetQuestionSeen() => _questionsSeen;

        public void IncrementQuestionsSeen() => _questionsSeen++;

        public int CalculateScore() => Mathf.RoundToInt(_correctAnswers / (float)_questionsSeen * 100);
    }
}