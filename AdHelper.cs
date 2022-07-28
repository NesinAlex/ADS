using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Devcell.Ads
{
    public class AdHelper : MonoBehaviour
    {
        [SerializeField]
        List<Button> adButtons;

        private void Start()
        {
            AdController.GetInstance()?.OnAdHelperReady(this);
        }

        private void OnDestroy()
        {
            AdController.GetInstance()?.OnAdHelperDestroyed();
        }

        public List<Button> GetRewardButtons() => adButtons;
    }
}
