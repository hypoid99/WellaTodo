using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellaTodo.DesignPattern
{
    public interface MobileAlertState
    {
        void alert(AlertStateContext ctx);
    }

    public class AlertStateContext
    {
        private MobileAlertState currentState;

        public AlertStateContext()
        {
            currentState = new Vibration();
        }

        public void setState(MobileAlertState state)
        {
            currentState = state;
        }

        public void alert()
        {
            currentState.alert(this);
        }
    }

    public class Vibration : MobileAlertState
    {
        public void alert(AlertStateContext ctx)
        {
            Console.WriteLine("Vibration");
        }
    }

    public class Slient : MobileAlertState
    {
        public void alert(AlertStateContext ctx)
        {
            Console.WriteLine("Silent");
        }

    }

    internal class State
    {
        AlertStateContext stateContext = new AlertStateContext();
        /*
        stateContext.alert();
        stateContext.setState(new Slient());
        stateContext.alert();
        */
    }
}
