using System;
using UnityEngine;

namespace Core
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

        [SerializeField]
        private SceneData[] scenes;

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
    }
}