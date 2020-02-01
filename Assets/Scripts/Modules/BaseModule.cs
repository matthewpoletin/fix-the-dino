using Core;
using Lifecycle;

namespace Modules
{
    public abstract class BaseModule : BaseView
    {
        public virtual void Activate(GameController gameController)
        {
        }

        public abstract void OnModuleLoaded();
    }
}