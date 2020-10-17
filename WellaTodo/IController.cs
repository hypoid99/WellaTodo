﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellaTodo
{
    public interface IController
    {
        IModel Get_Model();

        void Changed_View();

        void Initiate_View();
    }
}
