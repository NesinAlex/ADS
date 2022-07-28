using UnityEngine;
using UnityEngine.UI;

namespace Devcell.Ads
{
    public class AdsDemo : MonoBehaviour
    {
        [SerializeField]
        Button intersticalButton;

        void Start()
        {
            intersticalButton.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            AdController.GetInstance().ShowInterstitialAd();
        }
    }
}
