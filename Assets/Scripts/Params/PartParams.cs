using System;
using UnityEngine;

namespace Params
{
    [Serializable]
    public enum PartType
    {
        Tail = 0,
        Body = 1,
        Head = 2,
    }

    [CreateAssetMenu(fileName = "PartParams", menuName = "Params/PartParams", order = 30)]
    public class PartParams : ScriptableObject
    {
        [SerializeField] private PartType _partType = PartType.Tail;
        [SerializeField] private string _name = null;
        [SerializeField] private Sprite _boneImage = null;
        [SerializeField] private Sprite _fleshImage = null;

        public PartType PartType => _partType;
        public string Name => _name;
        public Sprite BoneImage => _boneImage;
        public Sprite FleshImage => _fleshImage;
    }
}