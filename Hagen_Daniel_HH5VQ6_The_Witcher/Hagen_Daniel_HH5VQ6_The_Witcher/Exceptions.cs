using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hagen_Daniel_HH5VQ6_The_Witcher
{
    class OutOfHazardRangeException : Exception
    {
        public OutOfHazardRangeException(string msg) : base(msg)
        {

        }
    }
    class ListIsEmptyException : Exception
    {
        public ListIsEmptyException(string msg) : base(msg)
        {

        }
    }

    class InvalidCityException : Exception
    {
        public InvalidCityException(string msg) : base(msg)
        {

        }
    }
}
