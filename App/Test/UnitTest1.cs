using App;
using Microsoft.VisualStudio.TestTools.UnitTesting;



namespace Test
{
    [TestClass]
    public class UnitTest1
    {

        #region hw+classwork
        [TestMethod]
        public void TestRomanNumberParse()
        {
            Dictionary<string, int> testCases = new()
            {
                {"II",      2},
                {"III",     3},
                {"IIII",    4},
                {"IV",      4},
                {"IX",      9},
                {"LXII",    62   },
                {"LXIII",   63   },
                {"LXIV",    64   },
                {"LXV",     65   },
                {"LXVI",    66   },
                {"LXVII",   67   },
                {"LXXXI",   81   },
                {"LXXXII",  82   },
                {"LXXXIII", 83   },
                {"LXXXIV",  84   },
                {"LXXXV",   85   },
                {"LXXXVI",  86   },
                {"V",       5    },
                {"VI",      6    },
                {"VII",     7    },
                {"VIII",    8    },
                {"VIIII",   9    },
                {"X",       10   },
                {"XI",      11   },
                {"XII",     12   },
                {"XIII",    13   },
                {"XIIII",   14   },
                {"XIIIII",  15   },
                {"XIV",     14   },
                {"XL",      40   },
                {"XLI",     41   },
                {"XLII",    42   },
                {"XLIII",   43   },
                {"XLIV",    44   },
                {"XLV",     45   },
                {"XV",      15   },
                {"XVI",     16   },
                {"XVII",    17   },
                {"XVIII",   18   },
                {"XX",      20   },
                {"XXIIIII", 25   },
                {"XXV",     25   },
                {"XXX",     30   },
                {"C",       100  },
                {"D",       500  },
                {"M",       1000 },
                {"IM",      999  },
                {"CM",      900  },
                {"XM",      990  },
                {"MCM",     1900 },
                {"N",       0    },
                {"-XLI",    -41  },
                {"-CLI",    -151 },
                {"-IL",     -49  },
                {"-XLIX",   -49  }
            };

            foreach (var pair in testCases)
            {
                Assert.AreEqual(pair.Value, RomanNumber.Parse(pair.Key).Value, $"{pair.Value} == {pair.Key}");
            }

            // Assert.AreEqual(1,RomanNumber.Parse("I").Value, "1 == I");
            // Assert.AreEqual(1,RomanNumber.Parse("I").Value, "2 == II");

            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInvalidRomanNumeral_WithInvalidSymbol()
        {
            RomanNumber.Parse("A");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInvalidRomanNumeral_WithLowerCaseLetter()
        {
            RomanNumber.Parse("xi");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInvalidRomanNumeral_WithNumber()
        {
            RomanNumber.Parse("X2V");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInvalidRomanNumeral_WithSpecialCharacter()
        {
            RomanNumber.Parse("X-V");
        }

        [TestMethod]
        public void TestParseException()
        {

            Assert.ThrowsException<ArgumentException>(
                () => RomanNumber.Parse(null!),
                "RomanNumber.Parse(null!) -> Exception"
            );
            Assert.ThrowsException<ArgumentException>(
                () => RomanNumber.Parse(""),
                "RomanNumber.Parse('') -> Exception"
            );
            //var ex =
            //    Assert.ThrowsException<ArgumentException>(
            //        () => RomanNumber.Parse("  "),
            //        "RomanNumber.Parse('  ') -> Exception"
            //    );


            // var ex = Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("XA"));
            //  Assert.IsTrue(ex.Message.Contains("Invalid character found: 'A'"), "Expected message to contain 'Invalid character found: 'A''");
            var ex = Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("XA"));
            Assert.IsTrue(ex.Message.Contains("Invalid character found: A."), "Expected message to contain 'Invalid character found: A.'");


            Assert.IsFalse(string.IsNullOrEmpty(ex.Message),
                "RomanNumber.Parse('') -- ex.Message not empty");

            Dictionary<string, char> testCases = new()
            {
                { "XA", 'A' },
                { "LB", 'B' },
                { "vI", 'v' },
                { "1X", '1' },
                { "$M", '$' },
                { "mX", 'm' },
                { "iM", 'i' }
            };
            foreach (var pair in testCases)
            {
                Assert.IsTrue(
                    Assert.ThrowsException<ArgumentException>(
                        () => RomanNumber.Parse("XA"),
                        "RomanNumber.Parse(XA) -> Exception"
                    ).Message.Contains("Invalid character found: A"),
                    "RomanNumber.Parse(XA): expected message to contain 'Invalid character found: A'"
                );

            }

            string num = "MAM";
            
            ex = Assert.ThrowsException<ArgumentException>(
                () => RomanNumber.Parse(num));

            // Assert.IsTrue(ex.Message.Contains("Invalid digit", StringComparison.OrdinalIgnoreCase), "ex.Message Contains 'Invalid digit' ");
            Assert.IsTrue(ex.Message.Contains("Invalid digit"), "ex.Message Contains 'Invalid digit'");


            Assert.IsTrue(
                ex.Message.Contains($"'{num}'"),
                $"ex.Message contains '{num}'"
            );

        }

        [TestMethod]
        public void TestParseInvalid()
        {
            Dictionary<string, char> testCases = new()
            {
                { "X C", ' ' },
                { "X\tC", '\t' },
                { "X\nC", '\n' },
            };
            foreach (var pair in testCases)
            {
                Assert.IsTrue(
                     Assert.ThrowsException<ArgumentException>(
                         () => RomanNumber.Parse(pair.Key),
                         $"RomanNumber.Parse({pair.Key}) -> Exception"
                     )
                     .Message.Contains($"'{pair.Value}'"),
                     $"RomanNumber.Parse({pair.Key}): ex.Message contains '{pair.Value}'"
                 );
            }

            Dictionary<string, char[]> testCases2 = new()
            {
                { "12XC",    new[] { '1', '2' } },
                { "XC12",    new[] { '1', '2' } },
                { "123XC",   new[] { '1', '2', '3' } },
                { "321X",    new[] { '3', '2', '1' } },
                { "3V2C1X",  new[] { '3', '2', '1' } },
            };
            foreach (var pair in testCases2)
            {
                var ex =Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse(pair.Key),$"Roman number parse {pair.Key} --> Exception");
                foreach (char c in pair.Value)
                {
                    //Assert.IsTrue(
                    //    ex.Message.Contains($"'{c}'"),
                    //    $"Roman number parse ({pair.Key}): ex.Message contains '{c}'"
                    // Assert.IsTrue(ex.Message.Contains($"'{pair.Value}'"),$"RomanNumber.Parse({pair.Key}): ex.Message contains '{pair.Value}'");
                    var expectedCharsAsString = string.Join(", ", pair.Value.Select(c => $"'{c}'"));
                    Assert.IsTrue(ex.Message.Contains(expectedCharsAsString), $"RomanNumber.Parse({pair.Key}): ex.Message contains {expectedCharsAsString}");

                }

            }
        }

        [TestMethod]
        public void TestParseDubious()
        {
            // Сумнівні випадки - на розгляд замовника
            String[] dubious = { " XC", "XC", "XC\n", "\tXC", " XC " };
            foreach (var str in dubious)
            {
                Assert.IsNotNull(RomanNumber.Parse(str),
                                $"Dubious '{str}' cause NULL");
            }
            
            int value = 90; 
            foreach (var str in dubious)
            {
                Assert.AreEqual(value, RomanNumber.Parse(str).Value, $"Dubious equality '{str}' --> '{value}'");
            }


            String[] dubious2 = { "IIX", "VVX" };
            foreach (var str in dubious2)
            {
                Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse(str), $"Dubious '{str}' cause Exception");

            }

        }

        [TestMethod]
        public void TestParseWithInvalidSymbols()
        {
            var testCases = new Dictionary<string, string>
            {
                 { "XA", "Invalid character(s) found: 'A'." },
                 { "VIIX", "Invalid structure found in input 'VIIX'." }, 
                 { "MCMXCv", "Invalid character(s) found: 'v'." },
                { "MMXVIII!", "Invalid character(s) found: '!'." },
            };

            foreach (var testCase in testCases)
            {
                ArgumentException? ex = Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse(testCase.Key));
                Assert.IsTrue(ex.Message.Contains(testCase.Value), $"Expected message to contain \"{testCase.Value}\", but got \"{ex.Message}\".");
            }
        }




        [TestMethod]
        public void TestToString()
        {
            Dictionary<int, String> testCases = new()
            {
                { 0,     "N"        },
                { 1,     "I"        },
                { 2,     "II"       },
                { 4,     "IV"       },
                { 9,     "IX"       },
                { 19,    "XIX"      },
                { 99,    "XCIX"     },
                { 499,   "CDXCIX"   },
                { 999,   "CMXCIX"   },
                { -45,   "-XLV"     },
                { -95,   "-XCV"     },
                { -285,  "-CCLXXXV" },
                { 1000,  "M"        },
                { 500,   "D"        },
                { 100,   "C"        },
                { 50,    "L"        },
                { 10,    "X"        },
                { 5,     "V"        },
                { 90,    "XC"       },
                { 40,    "XL"       },
                { 2000,  "MM"       },
                { 1050,  "ML"       },
                { 1115,  "MCXV"     },
                { 1400,  "MCD"      },
                { 1935,  "MCMXXXV"  },
                { 2023,  "MMXXIII"  },
            };

            foreach (var testCase in testCases)
            {
                Assert.AreEqual(
                    testCase.Value,
                    new RomanNumber(testCase.Key).ToString(),
                    $"{testCase.Key}.ToString() --> {testCase.Value}");
            }
        }

        [TestMethod]
        public void CrossTestParseToString()
        {
            for (int i = 1; i <= 3999; i++)
            {
                RomanNumber romanNumber = new RomanNumber { Value = i };
                
                string romanStr = romanNumber.ToString();
                
                RomanNumber parsedRomanNumber = RomanNumber.Parse(romanStr);
                Assert.AreEqual(i, parsedRomanNumber.Value, $"Failed at {i}: {romanStr}");
            }
        }

       [TestMethod]
		public void TypesFeature()
		{
			RomanNumber r = new(10);
            Assert.AreEqual(10L, r.Value); // 10u - uint, r.Value - int -- failed
            Assert.AreEqual((short)10, r.Value);
		}

        [TestMethod]
        public void TestAdd()
        {
            RomanNumber r1 = new(10);
            RomanNumber r2 = new(20);

            /*
            r1,Add(r2) - новый объект или измененный?
            var r3 = r1.Add(r2);
                - метод объекта должен модифицировать объект, иначе 
                должен быть метод класса RomanNumber, который возвращает новый объект

                рекомендуется immutable, особенно для числовых объектов
            
                Если RomanNumber - то новый. Хотим ли инструкции типа r1.Add(r2).Add(r3)...
                значит возврат есть, значит  - новый объект
            */

            var r3 = r1.Plus(r2);

            // Гарантия что Plus возвращает RomanNumber
            Assert.IsInstanceOfType(r1.Plus(r2), typeof(RomanNumber));
            // гарантия что это новый объект
            Assert.AreNotSame(r1, r3);
            Assert.AreNotSame(r2, r3);

            Assert.AreEqual(30,    r3.Value);
            Assert.AreEqual("XXX", r3.ToString());
            Assert.AreEqual(15,    r1.Plus(new(5)).Value);
            Assert.AreEqual(1,     r1.Plus(new(-9)).Value);
            Assert.AreEqual(-1,    r1.Plus(new(-11)).Value);
            Assert.AreEqual(0,     r1.Plus(new(-10)).Value);
            Assert.AreEqual(10,    r1.Plus(new()).Value);
            Assert.AreEqual(14,        RomanNumber.Parse("IV").Plus(new(10)).Value);
            Assert.AreEqual(-10,       RomanNumber.Parse("-V").Plus(new(-5)).Value);
            Assert.AreEqual("N",   new RomanNumber(41).Plus(new(-41)).ToString());
            Assert.AreEqual("-II", new RomanNumber(-32).Plus(new(30)).ToString());

            var ex = Assert.ThrowsException<ArgumentNullException>(() => r1.Plus(null!),
                "Plus(null!) -> ArgumentNullException"
            );

            String expectedFragment = "Illegal Plus() invocation with wull argument";
            Assert.IsTrue(ex.Message.Contains(expectedFragment,
                    StringComparison.InvariantCultureIgnoreCase
                ),
                $"Plus(null!): ex.Message ({ex.Message}) contains '{expectedFragment}'"
            );

        }

        [TestMethod]
        public void TestSum()
        {
            RomanNumber r1 = new(10);
            RomanNumber r2 = new(20);

            var r3 = RomanNumber.Sum(r1, r2);
            // Assert.IsInstanceOfType(r3, typeof(RomanNumber));
            Assert.IsNotNull(r3);

            Assert.AreEqual(60, RomanNumber.Sum(r1, r2, r3).Value);
            Assert.AreEqual( 0, RomanNumber.Sum().Value);
            Assert.AreEqual(0, RomanNumber.Sum(null!).Value); // Проверяем, что сумма равна 0
            Assert.AreEqual(40, RomanNumber.Sum(r1, null!, r3).Value);

            var arr1 = Array.Empty<RomanNumber>();
            var arr2 = new RomanNumber[] { new(2), new(4), new(5) };
            Assert.AreEqual(0,  RomanNumber.Sum(arr1).Value, "Empty arr --> Sum 0");
            Assert.AreEqual(11, RomanNumber.Sum(arr2).Value, "2-4-5 arr --> Sum 11");

            IEnumerable<RomanNumber> arr3 = new List<RomanNumber>() { new(2), new(4), new(5) };
            Assert.AreEqual(11, RomanNumber.Sum(arr3.ToArray()).Value, "2-4-5 list --> Sum 11");

            var arr4 = new RomanNumber[] { null!, null!, null! };
            // Assert.AreEqual(null, RomanNumber.Sum(arr4), "null! + null! + null! = null");

            Assert.AreEqual(0, RomanNumber.Sum(arr4).Value, "null! + null! + null! = 0");


            Random rnd = new();
            for (int i = 0; i < 200; i++)
            {
                int x = rnd.Next(-2000, 2000);
                int y = rnd.Next(-2000, 2000);
                Assert.AreEqual(x + y, RomanNumber.Sum(new(x), new(y)).Value, $"{x} + {y} random_t");
            }

            for (int i = 0; i < 200; i++)
            {
                RomanNumber rx = new(rnd.Next(-2000, 2000));
                RomanNumber ry = new(rnd.Next(-2000, 2000));
                Assert.AreEqual(rx.Plus(ry).Value, RomanNumber.Sum(rx, ry).Value, $"{rx} + {ry} random_t"
                );
            }
        }

        [TestMethod]
        public void TestSumWithLinqGeneratedCollections()
        {
            var numbers = Enumerable.Range(1, 5).Select(i => new RomanNumber(i)).ToArray();
            Assert.AreEqual(15, RomanNumber.Sum(numbers).Value, "Sum of 1-5 should be 15");

            var moreNumbers = Enumerable.Repeat(new RomanNumber(10), 3).ToList();
            Assert.AreEqual(30, RomanNumber.Sum(moreNumbers.ToArray()).Value, "Sum of three 10s should be 30");
        }

        [TestMethod]
        public void TestSumWithMultipleNullArguments()
        {
            var result = RomanNumber.Sum(null, null, null);

            // Assert.IsNull(RomanNumber.Sum(null, null, null), "Sum of multiple null arguments should be null");
            // Assert.IsNull(RomanNumber.Sum(new RomanNumber[] { null, null }), "Sum of null array should be null");
            Assert.IsNotNull(result); // Убедитесь, что результат не null
            Assert.AreEqual(0, result.Value); // Проверка, что сумма равна 0

        }

        [TestMethod]
        public void TestSumWithEmptyCollections()
        {
            Assert.AreEqual(0, RomanNumber.Sum(new RomanNumber[0]).Value, "Sum of empty array should be 0");
            Assert.AreEqual(0, RomanNumber.Sum(new RomanNumber[0]).Value, "Sum of empty list should be 0");
        }

        #endregion

        #region ПРАКТИКА
        // Тесты на корректные значения

        [TestMethod]
        public void TestEvalCorrectComputation()
        {
            Assert.AreEqual(3, RomanNumber.Eval("I + II").Value, "I + II should be III");
            Assert.AreEqual(5, RomanNumber.Eval("VII - II").Value, "VII - II should be V");
            // Проверка с одним операндом
            Assert.AreEqual(10, RomanNumber.Eval("X").Value, "X should return 10");
        }

        //[TestMethod]
        //public void Eval_NotNull()
        //{
        //    var result = RomanNumber.Eval("X + I");
        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void Eval_ReturnsCorrectType()
        //{
        //    var result = RomanNumber.Eval("V - I");
        //    Assert.IsInstanceOfType(result, typeof(RomanNumber));
        //}

        //[TestMethod]
        //public void Eval_CorrectCalculation()
        //{
        //    Assert.AreEqual(42, RomanNumber.Eval("XLII + 0").Value);
        //    Assert.AreEqual(39, RomanNumber.Eval("XLI - II").Value);
        //}

        //[TestMethod]
        //public void Eval_CrossTestWithPlusAndSum()
        //{
        //    var resultEval = RomanNumber.Eval("X + X");
        //    var resultPlus = new RomanNumber(10).Plus(new RomanNumber(10));
        //    var resultSum = RomanNumber.Sum(new RomanNumber(10), new RomanNumber(10));

        //    Assert.AreEqual(resultEval.Value, resultPlus.Value);
        //    Assert.AreEqual(resultEval.Value, resultSum.Value);
        //}


        // Тесты на некорректные значения
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEvalWithInvalidExpression()
        {
            RomanNumber.Eval("XL2 + II");
        }

        [TestMethod]
        public void TestEvalWithInvalidSymbols()
        {
            var ex = Assert.ThrowsException<ArgumentException>(() => RomanNumber.Eval("X + A"));
            Assert.IsTrue(ex.Message.Contains("Invalid expression"));
        }

        // Кросс-тестирование с Plus и Sum
        [TestMethod]
        public void CrossTestEvalWithPlusAndSum()
        {
            var evalResult = RomanNumber.Eval("X + X");
            var plusResult = new RomanNumber(10).Plus(new RomanNumber(10));
            var sumResult = RomanNumber.Sum(new RomanNumber(10), new RomanNumber(10));

            Assert.AreEqual(evalResult.Value, plusResult.Value, "Eval(X + X) should equal Plus(10, 10)");
            Assert.AreEqual(evalResult.Value, sumResult.Value, "Eval(X + X) should equal Sum(10, 10)");
        }
        #endregion
    }
}