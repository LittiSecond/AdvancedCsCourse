using System;
using System.Collections.Generic;

namespace OurGame
{

    /// <summary>
    /// Поведение программы
    /// </summary>
    abstract class BaseBehaviour
    {
        /// <summary>
        /// полный список объектов, требующих Update и Draw
        /// </summary>
        protected List<BaseObject> _objectsFullList;

        protected bool _enabled = false;

        public BaseBehaviour()
        {
            _objectsFullList = new List<BaseObject>();            
        }


        public virtual void Draw()
        {
            if (_enabled)
            {
                foreach (BaseObject o in _objectsFullList)
                {
                    o?.Draw();
                }
            }
        }

        public virtual void Update()
        {
            if (_enabled)
            {
                foreach (BaseObject o in _objectsFullList)
                {
                    o?.Update();
                }
            }
        }

        public virtual void On()
        {
            _enabled = true;
        }

        public virtual void Off()
        {
            _enabled = false;
        }
    }


}
