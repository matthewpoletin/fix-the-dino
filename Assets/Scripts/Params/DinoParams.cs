using UnityEngine;

namespace Params
{
    [CreateAssetMenu(fileName = "DinoParams", menuName = "Params/DinoParams", order = 20)]
    public class DinoParams : ScriptableObject
    {
        [SerializeField] private PartParams _headPart = null;
        [SerializeField] private PartParams _bodyPart = null;
        [SerializeField] private PartParams _tailPart = null;

        public PartParams HeadPart => _headPart;
        public PartParams BodyPart => _bodyPart;
        public PartParams TailPart => _tailPart;
    }
}