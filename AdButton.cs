using UnityEngine;
using UnityEngine.UI;

namespace Devcell.Ads
{
    public class AdButton : MonoBehaviour
    {
        private void Awake()
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
    }
}
