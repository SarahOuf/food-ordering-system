using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS_Project
{
    static class Global
    {
        private static int _globalvar = 0;

        public static int Globalvar
        {
            get { return _globalvar; }
            set { _globalvar = value; }
        }
    }
}
