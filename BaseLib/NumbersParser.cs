using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLib
{
    public static class NumbersParser
    {
        public static T Parse<T>(string input, string inputName = "Input string") where T : IConvertible
        {
            return ChangeType<T>(input, default(T).GetTypeCode(), inputName);
        }

        private static T ChangeType<T>(object input, TypeCode resultTypeCode, string inputName) where T : IConvertible
        {
            try
            {
                switch (resultTypeCode)
                {
                    case TypeCode.Int32:
                        input = int.Parse(input as string);
                        break;
                    case TypeCode.Double:
                        input = double.Parse(input as string);
                        break;
                    default:
                        throw new ArgumentException("Error parsing to format "
              + resultTypeCode + "! The specified format is not supported.");
                }

                return (T)Convert.ChangeType(input, resultTypeCode);
            }
            catch (Exception)
            {
                throw new FormatException(inputName + " isn't in the correct format!");
            }
        }
    }
}
