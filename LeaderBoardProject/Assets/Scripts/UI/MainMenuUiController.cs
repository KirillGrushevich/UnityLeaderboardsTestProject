using System;
using System.Threading.Tasks;
using Core.Interfaces;
using Object = UnityEngine.Object;

namespace UI
{
    public class MainMenuUiController
    {
        public MainMenuUiController(IGameInput gameInput, IScenesLoader scenesLoader, MainMenuUiView uiViewAsset)
        {
            this.gameInput = gameInput;
            this.scenesLoader = scenesLoader;
            this.uiViewAsset = uiViewAsset;
        }

        private IGameInput gameInput;
        private IScenesLoader scenesLoader;
        private MainMenuUiView uiViewAsset;

        private MainMenuUiView uiView;
        
        public async Task Show()
        {
            if (uiView == null)
            {
                uiView = Object.Instantiate(uiViewAsset);
            }

            //TODO - place animation here
            await Task.Delay(TimeSpan.FromSeconds(1f));
        }
    }
}