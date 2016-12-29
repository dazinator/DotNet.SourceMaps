using System;

namespace DotNet.SourceMaps
{


    public class Base64VariableLengthQuantityFormat

    {

        public static string Encode(int inValue)
        {

            // Add a new least significant bit that has the sign of the value.

            // if negative number the least significant bit that gets added to the number has value 1

            // else least significant bit value that gets added is 0

            // eg. -1 changes to binary : 01 [1] => 3

            //     +1 changes to binary : 01 [0] => 2

            if (inValue < 0)
            {

                inValue = ((-inValue) << 1) + 1;

            }

            else
            {

                inValue = inValue << 1;

            }



            // Encode 5 bits at a time starting from least significant bits

            var encodedStr = "";

            do
            {

                var currentDigit = inValue & 31; // 11111

                inValue = inValue >> 5;

                if (inValue > 0)
                {

                    // There are still more digits to decode, set the msb (6th bit)

                    currentDigit = currentDigit | 32;

                }

                encodedStr = encodedStr + Base64Format.Encode(currentDigit);

            } while (inValue > 0);



            return encodedStr;

        }



        public static object Decode(string inString)
        {

            var result = 0;

            var negative = false;



            var shift = 0;

            for (var i = 0; i < inString.Length; i++)
            {

                var thebyte = Base64Format.DecodeChar(inString.Substring(i, 1));

                if (i == 0)
                {

                    // Sign bit appears in the LSBit of the first value

                    if ((thebyte & 1) == 1)
                    {

                        negative = true;

                    }

                    result = (thebyte >> 1) & 15; // 1111x

                }

                else
                {

                    result = result | ((thebyte & 31) << shift); // 11111

                }



                shift += (i == 0) ? 4 : 5;



                if ((thebyte & 32) == 32)
                {

                    // Continue

                }

                else
                {

                    return new

                    {

                        value = negative ? -(result) : result,

                        rest = inString.Substring(i + 1)

                    };

                }

            }



            //throw new Error(getDiagnosticMessage(DiagnosticCode.Base64_value_0_finished_with_a_continuation_bit, [inString]));

            throw new Exception("Base64 value 0 finished with a continuation bit");

        }

    }
}