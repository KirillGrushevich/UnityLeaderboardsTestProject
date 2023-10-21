using System;
using System.Threading;
using System.Threading.Tasks;
using Extensions;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core.Configs
{
    [CreateAssetMenu(fileName = "GameMainData", menuName = "GameMainData", order = 0)]
    public class GameMainData : ScriptableObject
    {
        [Serializable]
        private class UiViewData
        {
            public GameScreen gameScreen;
            public AssetReferenceT<GameObject> viewAsset;
        }

        [Space(10)]
        [SerializeField] private UiViewData[] uiViews;
        

        public async Task<T> GetUiView<T>(GameScreen gameScreen, CancellationToken cancellationToken) where T : MonoBehaviour
        {
            foreach (var data in uiViews)
            {
                if (data.gameScreen != gameScreen)
                {
                    continue;
                }

                var gameObject = await data.viewAsset.LoadAssetAsync<GameObject>()
                    .Task.WithCancellation(cancellationToken);
 
                return gameObject.GetComponent<T>();

            }

            return null;
        }

    }
}