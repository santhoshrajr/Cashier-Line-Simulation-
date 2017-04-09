using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CashLineSimulator;

namespace CashLineSimulator
{
    /// <summary>
    /// GroceryHelper class contians functions for serving customers by selecting appropriate
    /// registers. It reads the file input and builds the customer objects.
    /// </summary>
   public class GroceryHelper
    {
       private static Queue<Customer> customerQueue = Grocery.getCustomerQueue();
        /// <summary>
        /// Gets all the customers which arrive at the same time to the registers
        /// </summary>
        /// <param name="customersAtSameTime"></param>
        /// <param name="time"></param>
        public static void getCustomersArrivingSameTime(
            List<Customer> customersAtSameTime,int time)
        {
            Customer customer = null;
            if(customerQueue.Count()>0)
            {
                customer = customerQueue.Peek();
            }
            while(customer!=null && customer.getTimeArrived()==time)
            {
                customersAtSameTime.Add(customerQueue.Dequeue());
                if (customerQueue.Count() > 0)
                    customer = customerQueue.Peek();
                else
                    customer = null;
                
            }
        }
        /// <summary>
        /// Expert Serve registers takes 1 minute to serve each item of customer
        /// </summary>
        /// <param name="customer"></param>
        public static void expertServe(Queue<Customer> customer)
        {
            Customer cust = null;
            if(customer.Count() >0)
            {
                cust = customer.Peek();
            }
            if(cust!=null && cust.removeItems()==0)
            {
                customer.Dequeue();
            }
        }
        /// <summary>
        /// Trainee Registers takes 2 minutes to serve each item of customer
        /// </summary>
        /// <param name="customer"></param>
        public static void traineeServe(Queue<Customer> customer)
        {
            Customer cust = null;
            if(customer.Count() >0)
            {
                cust = customer.Peek();
            }
            if(cust!=null )
            {
                if(cust.getServiceStatus()==false)
                {
                    cust.setServiceStatus(true);
                }
                else
                {
                    if(cust.removeItems()==0)
                    {
                        customer.Dequeue();
                    }
                    else
                    {
                        cust.setServiceStatus(false);
                    }
                }

            }
        }
        /// <summary>
        /// Building Inputs from the file using Stream reader.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Grocery readFromfile(string [] args)
        {
           
            Grocery grocery = null;
            RegisterFunctions registerFunctions = null;
            StreamReader streamReader = null;
            string line = " ";
            int firstline = 0;
            try
            {
                streamReader = new StreamReader(args[0]);

            }
            catch(FileNotFoundException e)
            {
                Console.WriteLine("File Not Found" + e.Message);
                Environment.Exit(-1);
            }
            try
            {
                while((line=streamReader.ReadLine())!=null)
                {
                    if(firstline==0)
                    {
                        int noOfRegisters = Convert.ToInt32(line);
                        registerFunctions = new RegisterFunctions(noOfRegisters);
                    }
                    else
                    {
                        Customer customer = buildCustomer(line);
                        customerQueue.Enqueue(customer);
                    }
                    firstline++;
                }
                grocery = new Grocery(registerFunctions);
            }
            catch(IOException e)
            {
                Console.WriteLine("Error reading Input" + e.Message);
                Environment.Exit(-1);
            }
            return grocery;
        }
        /// <summary>
        /// Building customer object based on the file Inputs.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static Customer buildCustomer(string line)
        {
            string[] partsOfline = line.Split(' ');
            if(partsOfline.Length!=3)
            {
                Console.WriteLine("Invalid Input");
                Environment.Exit(-1);
                
            }
             if(partsOfline[0].Equals(Type.A.ToString()))
            {
                return new Customer(Type.A, Convert.ToInt32(partsOfline[1]), Convert.ToInt32(partsOfline[2]));
            }
            else if(partsOfline[0].Equals(Type.B.ToString()))
            {
                return new Customer(Type.B, Convert.ToInt32(partsOfline[1]), Convert.ToInt32(partsOfline[2]));

            }
            else
            {
                Console.WriteLine("Customer Type is Invalid");
                
                Environment.Exit(-1);
                return null;

            }
            //throw new NotImplementedException();
        }
    }
}
