using System.Text;

namespace CommonBase
{
    public static class Number
    {
        static readonly Random Random = new(DateTime.Now.Millisecond);
        public static string CreateCreditCardNumber()
        {
            var result = new StringBuilder();
            var oddSum = 0;
            var evenSum = 0;
            int number;

            for (var i = 0; i < 7; i++)
            {
                number = Random.Next(0, 10);
                result.Append(number);
                evenSum += SumOfDigits(number * 2);

                number = Random.Next(0, 10);
                result.Append(number);
                oddSum += number;
            }
            number = Random.Next(0, 10);
            result.Append(number);
            evenSum += SumOfDigits(number * 2);

            var sum = evenSum + oddSum;

            if (sum % 10 == 0)
            {
                result.Append(0);
            }
            else
            {
                result.Append(10 - (sum % 10));
            }
            return result.ToString();
        }

        public static bool CheckCreditCardNumber(string number)
        {
            var result = number != null && number.Where(c => char.IsDigit(c)).Count() == 16;
            var oddSum = 0;
            var evenSum = 0;

            for (int i = 0; result && number != null && i < number.Length - 1; i++)
            {
                if (i % 2 == 0)
                    evenSum += SumOfDigits((number[i] - '0') * 2);
                else
                    oddSum += (number[i] - '0');
            }

            var sum = evenSum + oddSum;
            var rest = sum % 10 == 0 ? 0 : 10 - (sum % 10);

            return result && number != null && number[^1] - '0' == rest;
        }
        public static int SumOfDigits(int number)
        {
            var result = 0;

            while (number != 0)
            {
                result += number % 10;
                number /= 10;
            }
            return result;
        }
    }
}
