using System.Collections.Generic;
using QuizMaster.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace QuizMaster.Managers
{
    public class QuizManager : MonoBehaviour
    {
        [Header("Questions")]
        [SerializeField]
        private TextMeshProUGUI questionText;
        [SerializeField] private List<QuestionData> questions = new List<QuestionData>();
        private QuestionData currentQuestion;

        [Header("Answers")]
        [SerializeField]
        private GameObject[] answerButtons;

        private int correctAnswerIndex;
        private bool hasAnsweredEarly = true;

        [Header("Button Colors")]
        [SerializeField]
        private Sprite defaultAnswerSprite;
        [SerializeField] private Sprite correctAnswerSprite;

        [Header("Timer")]
        [SerializeField]
        private Image timerImage;

        private TimeManager _timeManager;

        [Header("Scoring")]
        [SerializeField]
        private TextMeshProUGUI scoreText;

        private ScoreManager _scoreManager;

        [Header("ProgressBar")]
        [SerializeField]
        private Slider progressBar;

        public bool isComplete;

        private void Awake()
        {
            _timeManager = FindObjectOfType<TimeManager>();
            _scoreManager = FindObjectOfType<ScoreManager>();
            progressBar.maxValue = questions.Count;
            progressBar.value = 0;
        }

        private void Update()
        {
            timerImage.fillAmount = _timeManager.fillFraction;
            
            if (_timeManager.loadNextQuestion)
            {
                if (progressBar.value == progressBar.maxValue)
                {
                    isComplete = true;
                    return;
                }

                hasAnsweredEarly = false;
                GetNextQuestion();
                _timeManager.loadNextQuestion = false;
            }
        
            else if (!hasAnsweredEarly && !_timeManager.isAnsweringQuestion)
            {
                DisplayAnswer(-1);
                SetButtonState(false);
            }
        }

        public void OnAnswerSelected(int index)
        {
            hasAnsweredEarly = true;
        
            DisplayAnswer(index);
            SetButtonState(false);
        
            _timeManager.CancelTimer();
        
            scoreText.text = "Score: " + _scoreManager.CalculateScore() + "%";
        }

        private void DisplayAnswer(int index)
        {
            Image buttonImage;
        
            if (index == currentQuestion.GetCorrectAnswerIndex())
            {
                questionText.text = "Correct!";
                buttonImage = answerButtons[index].GetComponent<Image>();
                buttonImage.sprite = correctAnswerSprite;
                _scoreManager.IncrementCorrectAnswers();
            }
        
            else
            {
                correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
                var correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
                questionText.text = "Sorry, the correct answer was;\n" + correctAnswer;
                buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
                buttonImage.sprite = correctAnswerSprite;
            }
        }

        private void GetNextQuestion()
        {
            if (questions.Count <= 0) return;
        
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
        
            progressBar.value++;
            _scoreManager.IncrementQuestionsSeen();
        }

        private void GetRandomQuestion()
        {
            var index = Random.Range(0, questions.Count);
            currentQuestion = questions[index];

            if (questions.Contains(currentQuestion))
            {
                questions.Remove(currentQuestion);
            }
        }

        private void DisplayQuestion()
        {
            questionText.text = currentQuestion.GetQuestion();

            for (var i = 0; i < answerButtons.Length; i++)
            {
                var buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = currentQuestion.GetAnswer(i);
            }
        }

        private void SetButtonState(bool state)
        {
            for (var i = 0; i < answerButtons.Length; i++)
            {
                var button = answerButtons[i].GetComponent<Button>();
                button.interactable = state;
            }
        }

        private void SetDefaultButtonSprites()
        {
            for (var i = 0; i < answerButtons.Length; i++)
            {
                var buttonImage = answerButtons[i].GetComponent<Image>();
                buttonImage.sprite = defaultAnswerSprite;
            }
        }
    }
}
