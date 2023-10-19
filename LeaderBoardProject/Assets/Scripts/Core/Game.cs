using System;
using System.Threading.Tasks;
using Core.Configs;
using Core.Interfaces;
using UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Core
{
    public class Game : IScenesLoader, IGameInput
    {
        public Game()
        {
            Init();
        }

        private GameMainData gameMainData;

        private EventFunctionsGO eventFunctionsGO;
        
        private GameInput gameInput;

        private MainMenuUiController mainMenuUiController;
        private LeaderBoardUiController leaderBoardUiController;

        private GameState gameState;

        public GameInput GameInput => gameInput;


        private async void Init()
        {
            gameMainData = await Addressables.LoadAssetAsync<GameMainData>(GameConstants.MAIN_CONFIG_ID).Task;

            var go = new GameObject("EventFunctionsGO");
            Object.DontDestroyOnLoad(go);
            eventFunctionsGO = go.AddComponent<EventFunctionsGO>();
            eventFunctionsGO.OnUpdateGO += Update;
            
            gameInput = new GameInput();

            var mainMenuViewAsset = await gameMainData.GetUiView<MainMenuUiView>(GameState.MainMenuScene,
                eventFunctionsGO.destroyCancellationToken);
            
            mainMenuUiController = new MainMenuUiController(this, this, mainMenuViewAsset);
            await mainMenuUiController.Show();
            
            gameState = GameState.MainMenuScene;
        }

        private void Update()
        {
            switch (gameState)
            {
                case GameState.MainMenuScene:
                    gameInput.Update();
                    break;
                
                case GameState.LeaderBoardScene:
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
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