using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLib
{
    public static class Functions
    {
        public static void CheckArrayMinimumLength<T>(T[] array, int minimumLength)
        {
            if (array.Length < minimumLength)
            {
                throw new FormatException(string.Format("Minimum count of parameters array is {0}!", minimumLength));
            }
        }
    }
}
