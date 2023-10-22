using System;

namespace Data
{
    [Serializable]
    public class Player
    {
        public string uid;
        public string username;
        public bool isVip;
        public string countryCode;
        public string characterColor;
        public uint characterIndex;

    }
    
    [Serializable]
    public class Ranking
    {
        public Player player;
        public uint ranking;
        public uint points;
    }
    
    [Serializable]
    public class LeaderBoardData
    {
        public Ranking[] ranking;
    }
}
