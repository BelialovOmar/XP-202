using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace App
{
    public class RomanNumber
    {
        #region classwork+homework
        private const  char   ZERO_DIGIT = 'N';
        private const  char   MINUS_SIGN = '-';
        private const  string INVALID_DIGIT_FORMAT      = "{0} '{1}'";
        private const  string INVALID_DIGITS_FORMAT     = "Pase error '{0}'. Ivalid digit(s): '{1}'";
        private const  string EMPTY_INPUT_MESSAGE       = "Empty or NULL input";
        private const  string INVALID_STRUCTURE_MESSAGE = "Invalid roman number structure";
        private const  string PLUS_NULL_ARGUMENT_MESSAGE = "Illegal Plus() invocation with wull argument";
        private static string DigitDecorator(char c)    => $"'{c}'";


        public int Value
        {
            get; set;
        }
        
        public RomanNumber(int initialValue = 0)
        {
            Value = initialValue;
        }


        private static int GetValueFromRomanChar(char romanChar)
        {
            return romanChar switch
            {
                'I' => 1,
                'V' => 5,
                'X' => 10,
                'L' => 50,
                'C' => 100,
                'D' => 500,
                'M' => 1000,
                'N' => 0,
                _ => throw new ArgumentException($"Invalid character found: {romanChar}")
                // _ => throw new ArgumentException()};
            };
        }

        public override string ToString()
        {
            if (Value == 0)
            {
                return "N";
            }

            Dictionary<int, String> ranges = new()
            {
                { 1000, "M"  },
                { 900,  "CM" },
                { 500,  "D"  },
                { 400,  "CD" },
                { 100,  "C"  },
                { 90,   "XC" },
                { 50,   "L"  },
                { 40,   "XL" },
                { 10,   "X"  },
                { 9,    "IX" },
                { 5,    "V"  },
                { 4,    "IV" },
                { 1,    "I"  },
            };
            
            StringBuilder result = new();
            int value = Math.Abs(Value);

            foreach (var range in ranges)
            {
                while (value >= range.Key)
                {
                    result.Append(range.Value);
                    value -= range.Key;
                }
            }
            return $"{(Value < 0 ? "-" : "")}{result}";
        }

        private static void CheckValidity(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Input cannot be null or empty.");
            }

            int firstDigitIndex = input[0] == '-' ? 1 : 0;
            if (input.Length == firstDigitIndex) // Если строка состоит только из знака минуса
            {
                throw new ArgumentException("Invalid input: string contains only a minus sign.");
            }

            for (int i = firstDigitIndex; i < input.Length; i++)
            {
                if (!IsValidRomanChar(input[i]))
                {
                    throw new ArgumentException($"Invalid character found: {input[i]}");
                }
            }
        }

        private static bool IsValidRomanChar(char c)
        {
            return c == 'I' || c == 'V' || c == 'X' || c == 'L' || c == 'C' || c == 'D' || c == 'M' || c == 'N';
        }


        private static void CheckValidityAndComposition(string input)
        {

            /*
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException(EMPTY_INPUT_MESSAGE);
            }

            bool isNegative = input.StartsWith(MINUS_SIGN);
            int maxDigit = 0;
            bool decreaseFlag = false; // Флаг для отслеживания убывания значения символов

            for (int i = isNegative ? 1 : 0; i < input.Length; i++)
            {
                int current;
                try
                {
                    current = GetValueFromRomanChar(input[i]);
                }
                catch (ArgumentException)
                {
                    throw new ArgumentException(String.Format(INVALID_DIGITS_FORMAT, input, input[i]));
                }

                if (current > maxDigit)
                {
                    maxDigit = current;
                    decreaseFlag = false; // Если мы увеличиваемся, сбрасываем флаг уменьшения
                }
                else if (current < maxDigit)
                {
                    if (decreaseFlag)
                    {
                        // Если мы снова уменьшаемся после уменьшения, бросает исключение
                        throw new ArgumentException(INVALID_STRUCTURE_MESSAGE);
                    }
                    decreaseFlag = true;
                }
            }
            
            */
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException(EMPTY_INPUT_MESSAGE);
            }

            string inputToCheck = input.StartsWith(MINUS_SIGN) ? input.Substring(1) : input;


            //if (!inputToCheck.All(c => IsValidRomanChar(c)))
            //{
            //    var invalidChars = inputToCheck.Where(c => !IsValidRomanChar(c))
            //            .Distinct()
            //            .Select(c => $"'{c}'")
            //            .Aggregate((current, next) => $"{current}, {next}");
            //    throw new ArgumentException($"Invalid character found: {invalidChars}.");
            //}

            if (!inputToCheck.All(c => IsValidRomanChar(c)))
            {
                var invalidChars = inputToCheck.Where(c => !IsValidRomanChar(c))
                .Distinct()
                .Select(c => $"'{c}'")
                .Aggregate((current, next) => $"{current}, {next}");
                throw new ArgumentException($"Invalid digit found: {invalidChars}.");
            }


        }


        public static RomanNumber Parse(String input)
        {
            if (input == null)
            {
                throw new ArgumentException(nameof(input), EMPTY_INPUT_MESSAGE);
                // throw new ArgumentNullException(nameof(input), EMPTY_INPUT_MESSAGE);
            }
            
            input = input?.Trim();
            
            CheckValidity(input);
            CheckValidityAndComposition(input);

            int firstDigitIndex = input.StartsWith(MINUS_SIGN) ? 1 : 0;
            int result = 0;
            int prev   = 0;
            
            for (int i = input.Length - 1; i >= firstDigitIndex; i--)
            {
                int current = GetValueFromRomanChar(input[i]);
                result      += (current < prev) ? -current : current;
                prev        = current;
            } 

            return new()
            {
                Value = firstDigitIndex == 0 ? result : -result
            };
        }

        public RomanNumber Plus(RomanNumber other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(PLUS_NULL_ARGUMENT_MESSAGE);
            }
            return new(this.Value + other.Value);
        }

        public static RomanNumber Sum(params RomanNumber[] numbers)
        {
            if (numbers == null || numbers.All(n => n == null))
            {
                return new RomanNumber(0);
                // return null;
            }
            
            if (numbers.Length > 0)
            {
                int nullableNumbersCount = 0;
                foreach (var number in numbers)
                {
                    if (number == null!)
                    {
                        nullableNumbersCount++;
                    }
                }

                if (nullableNumbersCount == numbers.Length)
                {
                    return null!;
                }
            }

            return new(numbers.Sum(number => number?.Value ?? 0));
        }

        #endregion

        #region ПРАКТИКА
        public static RomanNumber Eval(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                throw new ArgumentException("Expression cannot be null or empty.", nameof(expression));
            }

            // Проверяем, соответствует ли выражение шаблону с оператором
            var match = Regex.Match(expression.Trim(), @"^(?<operand1>[IVXLCDM]+)(\s*)(?<operator>[+-])?(\s*)(?<operand2>[IVXLCDM]+)?$");
            if (!match.Success)
            {
                throw new ArgumentException("Invalid expression format.", nameof(expression));
            }

            // Если выражение состоит только из одного операнда
            if (string.IsNullOrEmpty(match.Groups["operator"].Value))
            {
                return RomanNumber.Parse(match.Groups["operand1"].Value);
            }

            var operand1 = RomanNumber.Parse(match.Groups["operand1"].Value);
            var operand2 = RomanNumber.Parse(match.Groups["operand2"].Value ?? "0"); 
            switch (match.Groups["operator"].Value)
            {
                case "+":
                return new RomanNumber(operand1.Value + operand2.Value);
                case "-":
                return new RomanNumber(operand1.Value - operand2.Value);
                default:
                throw new ArgumentException($"Unsupported operator: {match.Groups["operator"].Value}");
            }
        }
        #endregion

    }
}
