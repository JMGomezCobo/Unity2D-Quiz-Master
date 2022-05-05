using UnityEngine;

namespace QuizMaster.Data
{
    [CreateAssetMenu(menuName = "Quiz Master", fileName = "New Question")]
    public class QuestionData : ScriptableObject
    {
        [TextArea(2,6)]
        [SerializeField] private string _question;
        [SerializeField] private string[] _answers = new string[4];
        [Space]
        [SerializeField] [Range(0, 3)] private int correctAnswerIndex;

        public string GetQuestion() => _question;

        public string GetAnswer(int index) => _answers[index];

        public int GetCorrectAnswerIndex() => correctAnswerIndex;
    }
}