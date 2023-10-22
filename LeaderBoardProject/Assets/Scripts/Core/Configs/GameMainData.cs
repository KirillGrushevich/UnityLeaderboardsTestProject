using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data;
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
        
        [Serializable]
        private class PlayerPlaceIcon
        {
            public uint place;
            public Sprite sprite;
        }

        [Space(10)]
        [SerializeField] private UiViewData[] uiViews;

        [SerializeField] private TextAsset[] leaderBoardsAssets;

        [SerializeField] private PlayerPlaceIcon[] placeIcons;

        [SerializeField] private Sprite[] flags;

        [SerializeField] private Sprite[] portraits;

        private int currentTestAsset;
        

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

        public LeaderBoardData GetLeaderBoardData()
        {
            var asset = leaderBoardsAssets[currentTestAsset];
            currentTestAsset++;
            if (currentTestAsset >= leaderBoardsAssets.Length)
            {
                currentTestAsset = 0;
            }

            var data = JsonUtility.FromJson<LeaderBoardData>(asset.text);

            var sortedRanking = data.ranking.OrderBy(r => r.ranking);
            data.ranking = sortedRanking.ToArray();
            
            return data;
        }

        public Sprite GetPlaceIcon(uint place)
        {
            var data = placeIcons.FirstOrDefault(i => i.place == place);
            return data?.sprite;
        }

        public bool TryGetFlag(string countryCode, out Sprite sprite)
        {
            sprite = flags.FirstOrDefault(f => f.name == countryCode);
            return sprite != null;
        }

        public bool TryGetPortrait(uint index, out Sprite sprite)
        {
            var arrayIndex = index - 1;
            if (arrayIndex >= portraits.Length)
            {
                sprite = null;
                return false;
            }

            sprite = portraits[arrayIndex];
            return true;
        }
    }
}