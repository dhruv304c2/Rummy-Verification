using TMPro;
using UnityEngine;

namespace Game
{
    public class GameLogger : MonoBehaviour
    {
        public TextMeshProUGUI text;
        public Color normalLogColor;
        public Color errorLogColor;

        public static void Log(string message)
        {
            Instance.text.text = message;
            Instance.text.color = Instance.normalLogColor;
        }
        
        public static void LogError(string message)
        {
            Instance.text.text = message;
            Instance.text.color = Instance.errorLogColor;
        }

        public static GameLogger Instance => FindObjectOfType<GameLogger>();
    }
}