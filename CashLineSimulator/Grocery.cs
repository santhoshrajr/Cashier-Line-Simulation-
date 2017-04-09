using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashLineSimulator
{  
    /// <summary>
    /// Grocery class is the start point of the porgram. It has information of all customers
    /// in global queue which are served by trainee or expert registers.
    /// </summary>
   public class Grocery
    {
        private static Queue<Customer> customerQueue = new Queue<Customer>();
       RegisterFunctions registerTotal = null;
       
        public Grocery(RegisterFunctions registerTotal)
        {
            this.registerTotal = registerTotal;
        }

        public static Queue<Customer> getCustomerQueue()
        {
            return customerQueue;
        }

        public RegisterFunctions getRegisterFunctons()
        {
            return registerTotal;
        }
        static void Main(string[] args)
        {
            Grocery grocery = GroceryHelper.readFromfile(args);
            RegisterFunctions registerSet = grocery.getRegisterFunctons();
            int time = finaltime(registerSet, grocery);
            Console.WriteLine("Finished at: t = " + time + " minutes");
           // Console.ReadLine();
        }
        /// <summary>
        /// Calculates final time till all customers are served.
        /// </summary>
        /// <param name="registerSet"></param>
        /// <param name="grocery"></param>
        /// <returns></returns>
        private static int finaltime(RegisterFunctions registerSet, Grocery grocery)
        {
            int time = 1;
            while(!(customerQueue.Count()==0) || registerSet.getRegisterstatus())
                {
                List<Customer> customersAtSameTime = new List<Customer>();
                GroceryHelper.getCustomersArrivingSameTime(customersAtSameTime, time);
                customersAtSameTime.Sort();
                registerSet.serviceCustomer(customersAtSameTime);
                int index = 0;
                while(index < registerSet.getRegisterList().Count())
                {
                    Queue<Customer> customer = registerSet.getRegisterList()[index].getCustomers();
                    if(index==registerSet.getRegisterList().Count()-1)
                    {
                        GroceryHelper.traineeServe(customer);
                    }
                    else
                    {
                        GroceryHelper.expertServe(customer);
                    }
                    index++;
                }
                time++;
            }
            return time;
        }
    }
}
