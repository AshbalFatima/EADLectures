using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncConsole
{
    public class TeaAsync
    {


        public async Task MakeTea()
        {
            var water = HeatWater();
            var tLeaf = AddTeaLeaf();
            var tM= AddMilk();

            var tS = AddSugar();
            Task.WaitAll(water,tLeaf,tM,tS);
            await AddToCup();
        }

        private async Task AddToCup()
        {
            //await Task.Delay(new Random().Next(1000, 9999));
            await Task.Delay(5000);
            Console.WriteLine("Add to cup");
            Console.WriteLine(String.Format("Thread  {0}", Thread.CurrentThread.ManagedThreadId.ToString()));
        }

        private async Task AddTeaLeaf()
        {
            //await Task.Delay(new Random().Next(1000, 9999));
            await Task.Delay(5000);
            Console.WriteLine("Adding Tea Leaf");
            Console.WriteLine(String.Format("Thread  {0}", Thread.CurrentThread.ManagedThreadId.ToString()));
        }

        private async Task AddSugar()
        {
            //await Task.Delay(new Random().Next(1000, 9999));
            await Task.Delay(5000);
            Console.WriteLine("Adding Sugar");
            Console.WriteLine(String.Format("Thread  {0}", Thread.CurrentThread.ManagedThreadId.ToString()));
        }

        private async Task AddMilk()
        {
            //await Task.Delay(new Random().Next(1000, 9999));
            await Task.Delay(5000);
            Console.WriteLine("Adding Milk");
            Console.WriteLine(String.Format("Thread  {0}", Thread.CurrentThread.ManagedThreadId.ToString()));
        }
        private async Task HeatWater()
        {
            //await Task.Delay(new Random().Next(1000, 9999));
            await Task.Delay(5000);

            this.WaterIsBioling();
        }
        private void WaterIsBioling()
        {
            Console.WriteLine("Water is Boiling");
            Console.WriteLine(String.Format("Thread  {0}", Thread.CurrentThread.ManagedThreadId.ToString()));
        }
    }
}
