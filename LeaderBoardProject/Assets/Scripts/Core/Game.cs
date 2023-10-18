using System.Threading.Tasks;
using Core.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Core
{
    public class Game : ISceneLoader
    {
        private static UiController uiController;

        public Game()
        {
            Init();
        }

        private async void Init()
        {
            var loader = Addressables.LoadAssetAsync<GameMainData>(GameConstants.MAIN_CONFIG_ID);

            await loader.Task;
            
            Debug.LogError(loader.Result.GetSceneId(GameState.LeaderBoardScene));
        }


        public async Task LoadSceneAsyncAdditive(int sceneIndex)
        {
            var loader = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);

            while (!loader.isDone)
            {
                await Task.Yield();
            }
        }

        public async Task UnloadSceneAsync(int sceneIndex)
        {
            var loader = SceneManager.UnloadSceneAsync(sceneIndex);

            while (!loader.isDone)
            {
                await Task.Yield();
            }
        }
    }
}