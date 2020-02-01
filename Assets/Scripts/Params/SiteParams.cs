using UnityEngine;

namespace Params
{
    [CreateAssetMenu(fileName = "SiteParams", menuName = "Params/SiteParams", order = 10)]
    public class SiteParams : ScriptableObject
    {
        [SerializeField] private string _name = null;

        public string Name => _name;
    }
}