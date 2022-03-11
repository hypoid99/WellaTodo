using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellaTodo
{
    /*
    public interface Strategy
    {
        void doStuff();

        StrategyName getStrategyName();
    }

    public enum StrategyName
    {
        StrategyA,
        StrategyB,
        StrategyC
    }

    public class StrategyA : Strategy
    {
        public void doStuff()
        {
            // 알고리즘 A 구현하기
        }

        public StrategyName getStrategyName()
        {
            return StrategyName.StrategyA;
        }
    }

    public class StrategyB : Strategy
    {
        public void doStuff()
        {
            // 알고리즘 B 구현하기
        }

        public StrategyName getStrategyName()
        {
            return StrategyName.StrategyB;
        }
    }

    public class StrategyC : Strategy
    {
        public void doStuff()
        {
            // 알고리즘 C 구현하기
        }

        public StrategyName getStrategyName()
        {
            return StrategyName.StrategyC;
        }
    }

    public class StrategyFactory
    {
        private Map<StrategyName, Strategy> strategies;

        public StrategyFactory(Set<Strategy> strategySet)
        {
            createStrategy(strategySet);
        }

        public Strategy findStrategy(StrategyName strategyName)
        {
            return strategies.get(strategyName);
        }

        private void createStrategy(Set<Strategy> strategySet)
        {
            strategies = new HashMap<StrategyName, Strategy>();
            strategySet.forEach(strategy->strategies.put(strategy.getStrategyName(), strategy));
        }
    }

    public class SomeService
    {
        private StrategyFactory strategyFactory;

        public void findSome()
        {
            // 이름을 전달해서 전략을 가져올 수 있다.
            Strategy strategy = strategyFactory.findStrategy(StrategyName.StrategyA);

            // 이제 전략에 정의된 메소드를 호출할 수 있다.
            strategy.doStuff();
        }
    }
    */

    public interface Strategy
    {
        bool isGoodMatch(StrategyName strategyName);
    }

    public class ImplementedStrategy1 : Strategy
    {
        public bool isGoodMatch(StrategyName strategyName)
        {
            return strategyName == StrategyName.ImplementedStrategy1;
        }
    }

    public class ImplementedStrategy2 : Strategy
    {
        public bool isGoodMatch(StrategyName strategyName)
        {
            return strategyName == StrategyName.ImplementedStrategy2;
        }
    }

    public class ImplementedStrategy3 : Strategy
    {
        public bool isGoodMatch(StrategyName strategyName)
        {
            return strategyName == StrategyName.ImplementedStrategy3;
        }
    }

    public enum StrategyName { ImplementedStrategy1, ImplementedStrategy2, ImplementedStrategy3 };

    public class StrategyFactory
    {

        private List<Strategy> strategies;

        public StrategyFactory(List<Strategy> strategies)
        {
            this.strategies = strategies;
        }

        public Strategy getInstance(StrategyName choice)
        {
            Strategy strategyChoice = null;

            foreach (Strategy strategy in this.strategies)
            {
                if (strategy.isGoodMatch(choice)) strategyChoice = strategy;
            }

            return strategyChoice;
        }
    }

}
