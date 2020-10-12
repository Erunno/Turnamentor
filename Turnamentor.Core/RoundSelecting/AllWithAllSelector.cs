using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Turnamentor.Core.RoundSelecting
{
    class AllWithAllSelector<Contestant> : IRoundSelector<Contestant>
    {
        private List<Type> contestants;
        private int roundSize;


        public AllWithAllSelector(RoundSelectorConfig config)
        {
            roundSize = config.NumberOfContestantsInRound;
            contestants = config.Contestants;
        }


        public IEnumerable<IList<Contestant>> GetAllRounds()
        {
            // the algorithm mimics recursion (real recursion would be inefficient)
            //  stack.Push is something like recursive call
            //  stack.Pop is return from fuction

            // you can read is as:
            //  
            //   if (currentContestants.Count == roundSize)
            //       yield return "Contestans for one round" 
            //   
            //   if (currIndex == contestants.Count)
            //       return;
            //   
            //   currentContestants.Add(allItems[currIndex]);
            //   RecursiveCall(currentIndex: currentIndex + 1);
            //   
            //   currentContestants.RemoveAt(currentItems.Count - 1);
            //   RecursiveCall(currentIndex: currentIndex + 1);

            List<Type> currentContestants = new List<Type>();
            Stack<Frame> stack = new Stack<Frame>(); // explicit "call" stack

            stack.Push(new Frame(0));

            while (stack.Count > 0)
            {
                var currFrame = stack.Peek();
                var currentIndex = currFrame.Index;

                switch (currFrame.Stage)
                {
                    case AlgStage.First:

                        if (currentContestants.Count == roundSize)
                        {
                            stack.Pop();
                            yield return currentContestants
                                .Select(type => (Contestant)Activator.CreateInstance(type))
                                .ToList();
                            continue;
                        }

                        if (currentIndex == contestants.Count)
                        {
                            stack.Pop();
                            continue;
                        }

                        currentContestants.Add(contestants[currentIndex]);
                        
                        currFrame.Stage = AlgStage.Second;
                        stack.Push(new Frame(currentIndex + 1));
                        continue;

                    case AlgStage.Second:

                        currentContestants.RemoveAt(currentContestants.Count - 1);

                        stack.Pop();
                        stack.Push(new Frame(currentIndex + 1));
                        continue;

                    default:
                        throw new NotImplementedException();
                }
            }
        }

        enum AlgStage { First, Second }

        class Frame
        {
            public Frame(int index)
            {
                Index = index;
            }
            public AlgStage Stage { get; set; } = AlgStage.First;
            public int Index { get; set; }
        }
    }
}
