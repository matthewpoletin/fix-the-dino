using Lifecycle;
using TMPro;
using UnityEngine;

namespace Modules.Combine
{
    public class DinoNameItemView : BaseView
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void SetData(string dinoName)
        {
            _text.text = dinoName;
        }
    }
}