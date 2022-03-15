using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellaTodo.DesignPattern
{
    public interface Observer
    {
        void update(int runs, int wickets, float overs);
    }

    public interface Subject
    {
        void registerObserver(Observer o);
        void unregisterObserver(Observer o);
        void notifyObservers();
    }

    public class AverageScoreDisplay : Observer
    {
        private float runRate;
        private int predictedScore;

        public void update(int runs, int wickets, float overs)
        {
            this.runRate = (float)runs / overs;
            this.predictedScore = (int)(this.runRate * 50);
            display();
        }

        public void display()
        {
            Console.WriteLine("\nAverage Score Display : " + "\nRun Rate : " + runRate + "\nPredictedScore : " + predictedScore);
        }
    }

    public class CurrentScoreDisplay : Observer
    {
        private int runs, wickets;
        private float overs;

        public void update(int runs, int wickets, float overs)
        {
            this.runs = runs;
            this.wickets = wickets;
            this.overs = overs;
            display();
        }

        public void display()
        {
            Console.WriteLine("\nCurrent Score Display : " + "\nRuns: " + runs + "\nWickets:" + wickets + "\nOvers: " + overs);
        }
    }

    public class CircketData : Subject
    {
        int runs;
        int wickets;
        float overs;
        List<Observer> observerList;

        public CircketData()
        {
            observerList = new List<Observer>();
        }

        public void registerObserver(Observer o)
        {
            observerList.Add(o);
        }

        public void unregisterObserver(Observer o)
        {
            observerList.Remove(o);
        }

        public void notifyObservers()
        {
            foreach (Observer o in observerList)
            {
                o.update(runs, wickets, overs);
            }
        }

        private int getLatestRuns()
        {
            return 90;
        }

        private int getLatestWickets()
        {
            return 2;
        }

        private float getLatestOvers()
        {
            return (float)10.2;
        }

        public void dataChanged()
        {
            runs = getLatestRuns();
            wickets = getLatestWickets();
            overs = getLatestOvers();

            notifyObservers();
        }
    }

    internal class ObserverPattern
    {
        AverageScoreDisplay averageScoreDisplay = new AverageScoreDisplay();
        CurrentScoreDisplay currentScoreDisplay = new CurrentScoreDisplay();
        CircketData circketData = new CircketData();
        /*
        circketData.registerObserver(averageScoreDisplay);
        circketData.registerObserver(currentScoreDisplay);
        circketData.dataChanged();
        Console.WriteLine("\n--------- Remove AverageScoreDisplay ---------");
        circketData.unregisterObserver(averageScoreDisplay);
        circketData.dataChanged();
        */
    }
}
