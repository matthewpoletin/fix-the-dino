using System.Collections.Generic;
using UnityEngine;

namespace Params
{
    [CreateAssetMenu(fileName = "ReviewParams", menuName = "Params/ReviewParams", order = 35)]
    public class ReviewParams : ScriptableObject
    {
        [SerializeField] private List<string> _lines = null;

        public List<string> Lines => _lines;
    }
}