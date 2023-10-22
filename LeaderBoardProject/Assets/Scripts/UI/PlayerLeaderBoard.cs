using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerLeaderBoard : MonoBehaviour
    {
        [field:SerializeField] public TMP_Text NumberId{ get; private set; }
        [field:SerializeField] public Image MedalImage{ get; private set; }
        [field:SerializeField] public Image PortraitBackground{ get; private set; }
        [field:SerializeField] public Image PortraitImage{ get; private set; }
        [field:SerializeField] public Image FlagImage{ get; private set; }
        [field:SerializeField] public TMP_Text PlayerName{ get; private set; }
        [field:SerializeField] public TMP_Text ScoreLabel{ get; private set; }
        [field:SerializeField] public GameObject CupGameObject{ get; private set; }

    }
}