using System;
using System.Collections.Generic;
using Core.Configs;
using Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LeaderBoardUiView : MonoBehaviour
    {
        [SerializeField] private Button closeButton;
        [SerializeField] private PlayerLeaderBoard leaderBoardTemplate;

        private readonly List<PlayerLeaderBoard> players = new();

        public event Action OnScreenClosed;

        public void Setup(LeaderBoardData leaderBoardData, GameMainData gameMainData)
        {
            gameObject.SetActive(true);
            
            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(CloseScreen);
            
            foreach (var playerLeaderBoard in players)
            {
                Destroy(playerLeaderBoard.gameObject);
            }
            
            players.Clear();
            
            leaderBoardTemplate.gameObject.SetActive(true);
            foreach (var ranking in leaderBoardData.ranking)
            {
                var newPlayer = Instantiate(leaderBoardTemplate, leaderBoardTemplate.transform.parent);
                newPlayer.NumberId.text = ranking.ranking.ToString();
                
                var medalSprite = gameMainData.GetPlaceIcon(ranking.ranking);
                if (medalSprite != null)
                {
                    newPlayer.MedalImage.sprite = medalSprite;
                }
                else
                {
                    newPlayer.MedalImage.gameObject.SetActive(false);
                }

                var colorString = ranking.player.characterColor;
                if (!colorString.Contains('#'))
                {
                    colorString = $"#{colorString}";
                }
                
                if (ColorUtility.TryParseHtmlString(colorString, out var color))
                {
                    newPlayer.PortraitBackground.color = color;
                }

                if (gameMainData.TryGetPortrait(ranking.player.characterIndex, out var portraitSprite))
                {
                    newPlayer.PortraitImage.sprite = portraitSprite;
                }
                else
                {
                    newPlayer.PortraitImage.color = Color.black;
                }

                if (gameMainData.TryGetFlag(ranking.player.countryCode, out var flagSprite))
                {
                    newPlayer.FlagImage.sprite = flagSprite;
                }
                else
                {
                    newPlayer.FlagImage.gameObject.SetActive(false);
                }
                
                
                newPlayer.PlayerName.text = ranking.player.username;
                
                var points = ranking.points;
                var pointsString = points.ToString("#,#").Replace(',' , ' ');
                newPlayer.ScoreLabel.text = pointsString;
                
                newPlayer.CupGameObject.SetActive(ranking.player.isVip);
                
                players.Add(newPlayer);
            }

            leaderBoardTemplate.gameObject.SetActive(false);
        }

        private void CloseScreen()
        {
            gameObject.SetActive(false);
            
            OnScreenClosed?.Invoke();
        }
    }
}