using System;
using UnityEngine;

namespace Lifecycle
{
    public abstract class BaseView : MonoBehaviour, ITick, IDisposable
    {
        public virtual void Tick(float deltaTime)
        {
        }

        public virtual void Dispose()
        {
        }

        public virtual void CleanUp()
        {
        }
    }
}