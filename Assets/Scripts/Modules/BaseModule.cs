using Core;
using Lifecycle;
using UnityEngine;

namespace Modules
{
    public abstract class BaseModule : BaseView
    {
        public virtual void Activate(GameController gameController, Canvas canvas)
        {
        }

        public abstract void OnModuleLoaded();
    }
}