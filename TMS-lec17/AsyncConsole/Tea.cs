using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncConsole
{
    internal class Tea
    {



        public void MakeTea()
        {
            HeatWater();
            AddTeaLeaf();
            AddMilk();
            
            AddSugar();
            AddToCup();
        }

        private void AddToCup()
        {
            
            Console.WriteLine("Add to cup");
        }

        private void AddTeaLeaf()
        {
            Thread.Sleep(5000);
            Console.WriteLine("Adding Tea Leaf");
        }

        private void AddSugar()
        {
            Thread.Sleep(5000);
            Console.WriteLine("Adding Sugar");
        }

        private void AddMilk()
        {
            Thread.Sleep(5000);
            Console.WriteLine("Adding Milk");
        }
        private void HeatWater()
        {
            //            Thread.Sleep(new Random().Next(1000,9999));
            Thread.Sleep(5000);
            this.WaterIsBioling();
        }
        private void WaterIsBioling()
        {
            Thread.Sleep(5000);
            Console.WriteLine("Water is Boiling");
        }

    }
}
