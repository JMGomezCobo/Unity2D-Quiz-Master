using UnityEngine;
using UnityEngine.SceneManagement;

namespace QuizMaster.Managers
{
    public class GameManager : MonoBehaviour
    {
        private QuizManager _quizManager;
        private EndScreen endScreen;

        private void Awake()
        {
            _quizManager = FindObjectOfType<QuizManager>();
            endScreen = FindObjectOfType<EndScreen>();
        }

        private void Start()
        {
            _quizManager.gameObject.SetActive(true);
            endScreen.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (!_quizManager.isComplete) return;
        
            _quizManager.gameObject.SetActive(false);
        
            endScreen.gameObject.SetActive(true);
            endScreen.ShowFinalScore();
        }

        public void OnReplayLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}