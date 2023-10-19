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
        private class SceneData
        {
            public GameState gameState;
            public uint sceneIndex;
        }
        
        [Serializable]
        private class UiViewData
        {
            public GameState gameState;
            public AssetReferenceT<GameObject> viewAsset;
        }

        [SerializeField] private SceneData[] scenes;
        [Space(10)]
        [SerializeField] private UiViewData[] uiViews;

        public uint GetSceneId(GameState gameState)
        {
            foreach (var sceneData in scenes)
            {
                if (sceneData.gameState != gameState)
                {
                    continue;
                }

                return sceneData.sceneIndex;
            }

            return 0;
        }

        public async Task<T> GetUiView<T>(GameState gameState, CancellationToken cancellationToken) where T : MonoBehaviour
        {
            foreach (var data in uiViews)
            {
                if (data.gameState != gameState)
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