using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Turnamentor.Core.RoundSelecting;

namespace Turnamentor.Core.Tests.Selectors
{
    public class AllWithAllSelectorTest
    {
        [Test]
        public void Test1()
        {
            // arrange
            var confing = new RoundSelectorConfig
            {
                Contestants = new List<Type>() { typeof(A), typeof(B), typeof(C), typeof(D) },
                NumberOfContestantsInRound = 2
            };
            var selector = new AllWithAllSelector<I>(confing);

            // act
            var allRounds = selector.GetAllRounds()
                .Select(x => x.Cast<I>()).ToList();

            // assert
            var allRoundsStrings = allRounds
                .Select(contestants =>
                {
                    var sb = new StringBuilder();
                    var sorted = contestants.OrderBy(x => x.Id);

                    foreach (var contestant in contestants)
                        sb.Append(contestant.Id);

                    return sb.ToString();
                })
                .OrderBy(x => x)
                .ToList();

            var expected = new List<string>
            {
                "AB", "AC", "AD", "BC", "BD", "CD"
            };

            CollectionAssert.AreEqual(expected, allRoundsStrings);
        }
    }


    // mock classes

    interface I { string Id { get; } }

    class A : I { public string Id => "A"; }
    class B : I { public string Id => "B"; }
    class C : I { public string Id => "C"; }
    class D : I { public string Id => "D"; }
}