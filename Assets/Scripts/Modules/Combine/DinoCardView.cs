using Lifecycle;
using Params;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Combine
{
    public class DinoCardView : BaseView
    {
        [SerializeField] private TextMeshProUGUI _nameText = null;
        [SerializeField] private Image _tailImage = null;
        [SerializeField] private Image _bodyImage = null;
        [SerializeField] private Image _headImage = null;
        [SerializeField] private TextMeshProUGUI _aiText = null;

        public void SetData(PartParams tailParams, PartParams bodyParams, PartParams headParams, ReviewParams reviews)
        {
            _nameText.text = $"{headParams.Name}{bodyParams.Name}{tailParams.Name}";
            _tailImage.sprite = tailParams.FleshImage;
            _bodyImage.sprite = bodyParams.FleshImage;
            _headImage.sprite = headParams.FleshImage;

            _aiText.text = reviews.Lines[Random.Range(0, reviews.Lines.Count)];
        }

        public override void CleanUp()
        {
        }
    }
}