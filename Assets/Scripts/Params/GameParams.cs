using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Params
{
    [CreateAssetMenu(fileName = "GameParams", menuName = "Params/GameParams", order = 0)]
    public class GameParams : ScriptableObject
    {
        [SerializeField] private float _siteDuration = 20f;
        [SerializeField] private float _combineDuration = 20f;

        [SerializeField] private List<SiteParams> _allSites = null;
        [SerializeField] private List<DinoParams> _allDinos = null;
        [SerializeField] private ReviewParams _reviewParams = null;

        public float SiteDuration => _siteDuration;
        public float CombineDuration => _combineDuration;

        public List<SiteParams> AllSites => _allSites;
        public List<DinoParams> AllDinos => _allDinos;
        public ReviewParams ReviewParams => _reviewParams;

        public List<PartParams> GetAllParts()
        {
            var result = new List<PartParams>();
            foreach (var dinoParams in AllDinos)
            {
                result.Add(dinoParams.HeadPart);
                result.Add(dinoParams.BodyPart);
                result.Add(dinoParams.TailPart);
            }

            return result.Where(part => part != null).ToList();
        }
    }
}