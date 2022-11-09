using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class StartGameButton : MonoBehaviour
    {
        public int gameStartScene;

        public void StartGame()
        {
            SceneManager.LoadScene(gameStartScene);
        }
    }
}
