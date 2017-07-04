using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace ADND
{
    public interface IAction 
    {
        void LoadAction();
        void TriggerAction();
    }
}