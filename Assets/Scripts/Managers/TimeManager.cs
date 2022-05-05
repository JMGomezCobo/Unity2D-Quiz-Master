using UnityEngine;

namespace QuizMaster.Managers
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private float _timeToCompleteQuestion = 30f;
        [SerializeField] private float _timeToShowCorrectAnswer = 10f;

        public bool loadNextQuestion;
        public float fillFraction;
        public bool isAnsweringQuestion;

        private float _timerValue;

        private void Update()
        {
            UpdateTimer();
        }

        public void CancelTimer()
        {
            _timerValue = 0;
        }

        private void UpdateTimer()
        {
            _timerValue -= Time.deltaTime;

            if (isAnsweringQuestion)
            {
                if (_timerValue > 0)
                {
                    fillFraction = _timerValue / _timeToCompleteQuestion;
                }
            
                else
                {
                    isAnsweringQuestion = false;
                    _timerValue = _timeToShowCorrectAnswer;
                }
            }
        
            else
            {
                if (_timerValue > 0)
                {
                    fillFraction = _timerValue / _timeToShowCorrectAnswer;
                }
            
                else
                {
                    isAnsweringQuestion = true;
                    _timerValue = _timeToCompleteQuestion;
                    loadNextQuestion = true;
                }
            }
        }
    }
}