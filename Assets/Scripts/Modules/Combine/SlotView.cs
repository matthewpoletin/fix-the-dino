using Lifecycle;
using Params;
using UnityEngine;

namespace Modules.Combine
{
    public class SlotView : BaseView
    {
        [SerializeField] private PartType _slotType = PartType.Tail;

        public PartType SlotType => _slotType;

        public bool HasItem => transform.childCount > 0;
    }
}