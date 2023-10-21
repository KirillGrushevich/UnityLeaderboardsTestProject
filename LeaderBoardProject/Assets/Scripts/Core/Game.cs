using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Configs;
using Core.Interfaces;
using UI;
using UI.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace Core
{
    public class Game : IScreenLoader
    {
        public Game()
        {
            Init();
        }

        private GameMainData gameMainData;

        private EventFunctionsGO eventFunctionsGo;
        
        private GameInput gameInput;

        private MainMenuUiController mainMenuUiController;
        private LeaderBoardUiController leaderBoardUiController;

        private GameScreen currentGameScreen;

        private Dictionary<GameScreen, IUiController> uiControllers;


        private async void Init()
        {
            gameMainData = await Addressables.LoadAssetAsync<GameMainData>(GameConstants.MAIN_CONFIG_ID).Task;

            var go = new GameObject("EventFunctionsGO");
            Object.DontDestroyOnLoad(go);
            eventFunctionsGo = go.AddComponent<EventFunctionsGO>();
            eventFunctionsGo.OnUpdateGO += Update;
            
            gameInput = new GameInput();

            uiControllers = new Dictionary<GameScreen, IUiController>
            {
                {GameScreen.MainMenuScreen, new MainMenuUiController(
                    gameInput, this, gameMainData, eventFunctionsGo.destroyCancellationToken)},
                
                {GameScreen.LeaderBoardScreen, new LeaderBoardUiController(
                    gameInput, this, gameMainData, eventFunctionsGo.destroyCancellationToken)},
            };

            await LoadScreen(GameScreen.MainMenuScreen);
            
            currentGameScreen = GameScreen.MainMenuScreen;
        }

        private void Update()
        {
            switch (currentGameScreen)
            {
                case GameScreen.MainMenuScreen:
                    gameInput.Update();
                    break;
                
                case GameScreen.LeaderBoardScreen:
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public async Task LoadScreen(GameScreen gameScreen)
        {
            if (!uiControllers.ContainsKey(gameScreen))
            {
                Debug.LogError($"Screen {gameScreen} is not found");
                return;
            }

            await uiControllers[gameScreen].Show();
        }
    }
}