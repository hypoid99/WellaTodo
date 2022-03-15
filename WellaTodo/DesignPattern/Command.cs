using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellaTodo.DesignPattern
{
    public interface Command
    {
        void execute();
    }

    public class Light
    {
        public void on()
        {
            Console.WriteLine("Light is on");
        }

        public void off()
        {
            Console.WriteLine("Light is off");
        }
    }

    public class LightOffCommand : Command
    {
        Light light;

        public LightOffCommand(Light light)
        {
            this.light = light;
        }

        public void execute()
        {
            light.off();
        }
    }

    public class LightOnCommand : Command
    {
        Light light;

        public LightOnCommand(Light light)
        {
            this.light = light;
        }

        public void execute()
        {
            light.on();
        }
    }

    public class SimpleRemoteControl
    {
        Command slot;

        public SimpleRemoteControl() { }

        public void setCommand(Command command)
        {
            slot = command;
        }

        public void buttonWasPressed()
        {
            slot.execute();
        }
    }

    public class Stereo
    {
        public void on()
        {
            Console.WriteLine("Stereo is on");
        }
        public void off()
        {
            Console.WriteLine("Stereo is off");
        }
        public void setCD()
        {
            Console.WriteLine("Stereo is set for CD input");
        }
        public void setDVD()
        {
            Console.WriteLine("Stereo is set for DVD input");
        }
        public void setRadio()
        {
            Console.WriteLine("Stereo is set for Radio input");
        }
        public void setVolume(int volume)
        {
            Console.WriteLine("Stereo Volume set to " + volume);
        }
    }

    public class StereoOnWithCDCommand : Command
    {
        Stereo stereo;

        public StereoOnWithCDCommand(Stereo stereo)
        {
            this.stereo = stereo;
        }

        public void execute()
        {
            stereo.on();
            stereo.setCD();
            stereo.setVolume(11);
        }

    }

    internal class CommandPattern
    {
        SimpleRemoteControl remote = new SimpleRemoteControl();
        Light light = new Light();
        Stereo stereo = new Stereo();
        /*
        remote.setCommand(new LightOnCommand(light));
        remote.buttonWasPressed();
        remote.setCommand(new StereoOnWithCDCommand(stereo));
        remote.buttonWasPressed();
        remote.setCommand(new StereoOffCommand(stereo));
        remote.buttonWasPressed();
        */
    }
}
