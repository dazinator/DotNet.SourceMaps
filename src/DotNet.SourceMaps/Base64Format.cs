using System;

namespace DotNet.SourceMaps
{

    public class Base64Format

    {

        static string encodedValues = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";



        public static string Encode(int inValue)
        {

            if (inValue < 64)
            {
                return Base64Format.encodedValues.Substring(inValue, 1);
            }

            throw new ArgumentException(inValue + ": not a 64 based value");

        }



        public static int DecodeChar(string inChar)

        {

            if (inChar.Length == 1)

            {

                return Base64Format.encodedValues.IndexOf(inChar);

            }

            else

            {

                throw new ArgumentException("'" + inChar + "' must have length 1");

            }

        }

    }
}