using System;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Configs;
using Core.Interfaces;
using UI.Interfaces;
using Object = UnityEngine.Object;

namespace UI
{
    public class MainMenuUiController : IUiController
    {
        public MainMenuUiController(
            IGameInput gameInput, 
            IScreenLoader screenLoader, 
            GameMainData gameMainData, 
            CancellationToken cancellationToken)
        {
            this.gameInput = gameInput;
            this.screenLoader = screenLoader;
            this.gameMainData = gameMainData;
            this.cancellationToken = cancellationToken;
        }

        private readonly IGameInput gameInput;
        private readonly IScreenLoader screenLoader;
        private readonly GameMainData gameMainData;
        private readonly CancellationToken cancellationToken;
        
        private MainMenuUiView uiViewAsset;

        private MainMenuUiView uiView;

        private void OnScreenTapped()
        {
            gameInput.OnScreenTapped -= OnScreenTapped;

            screenLoader.LoadScreen(GameScreen.LeaderBoardScreen);
        }

        public async Task Show()
        {
            if (uiViewAsset == null)
            {
                uiViewAsset = await gameMainData.GetUiView<MainMenuUiView>(GameScreen.MainMenuScreen, cancellationToken);
            }
            
            if (uiView == null)
            {
                uiView = Object.Instantiate(uiViewAsset);
            }

            //TODO - place animation here
            await Task.Delay(TimeSpan.FromSeconds(1f), cancellationToken);

            gameInput.OnScreenTapped += OnScreenTapped;
        }

    }
}