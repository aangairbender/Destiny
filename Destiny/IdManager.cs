using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destiny
{
    class IdManager
    {
        private int counter;
        public IdManager()
        {
            counter = 0;
        }
        public int next()
        {
            return counter++;
        }
    }
}
