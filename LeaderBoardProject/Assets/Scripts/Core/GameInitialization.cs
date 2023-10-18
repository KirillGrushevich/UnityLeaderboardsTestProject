using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public static class GameInitialization
    {
        private static Game game;
        
        [RuntimeInitializeOnLoadMethod]
        public static async void Initialization()
        {
            while (SceneManager.loadedSceneCount == 0)
            {
                await Task.Yield();
            }

#if UNITY_EDITOR
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                var loader = SceneManager.LoadSceneAsync(0);

                while (!loader.isDone)
                {
                    await Task.Yield();
                }
            }
#endif
            game = new Game();
        }
    }
}