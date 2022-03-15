using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellaTodo.DesignPattern
{
    public interface Chain
    {
        void setNext(Chain nextInChain);
        void process(Number request);
    }

    public class Number
    {
        private int number;

        public Number(int number)
        {
            this.number = number;
        }
        public int getNumber()
        {
            return number;
        }
    }

    public class NegativeProcessor : Chain
    {
        private Chain nextInChain;

        public void setNext(Chain nextInChain)
        {
            this.nextInChain = nextInChain;
        }

        public void process(Number request)
        {
            if (request.getNumber() < 0)
            {
                Console.WriteLine("NegativeProcessor : " + request.getNumber());
            }
            else
            {
                nextInChain.process(request);
            }
        }
    }

    public class ZeroProcessor : Chain
    {
        private Chain nextInChain;

        public void setNext(Chain nextInChain)
        {
            this.nextInChain = nextInChain;
        }

        public void process(Number request)
        {
            if (request.getNumber() == 0)
            {
                Console.WriteLine("ZeroProcessor : " + request.getNumber());
            }
            else
            {
                nextInChain.process(request);
            }
        }
    }

    public class PositiveProcessor : Chain
    {
        private Chain nextInChain;

        public void setNext(Chain nextInChain)
        {
            this.nextInChain = nextInChain;
        }

        public void process(Number request)
        {
            if (request.getNumber() > 0)
            {
                Console.WriteLine("PositiveProcessor : " + request.getNumber());
            }
            else
            {
                nextInChain.process(request);
            }
        }
    }

    public class Client
    {
        Chain c1 = new NegativeProcessor();
        Chain c2 = new ZeroProcessor();
        Chain c3 = new PositiveProcessor();

        /*
        c1.setNext(c2);
        c2.setNext(c3);
        
        c1.process(new Number(90));
        c1.process(new Number(-50)); 
        c1.process(new Number(0)); 
        c1.process(new Number(91)); 
        */
    }
}
