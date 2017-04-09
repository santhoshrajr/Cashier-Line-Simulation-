using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashLineSimulator
{
    /// <summary>
    /// This class contains information about Register which has Id and Queue of customers
    /// to serve. 
    /// Comparators are used to compare Ids of Register which enables sorting so that we can 
    /// identify training and expert register.
    /// </summary>
    public class Register:IComparable<Register>
    {
        private int id;
        private Queue<Customer> customerQueue=null;
        public Register(int id)
        {
            this.id = id;
            customerQueue = new Queue<Customer>();
        }
        public int getId()
        {
            return id;
        }
        public Queue<Customer> getCustomers()
        {
            return customerQueue;
        }
        /// <summary>
        /// Comparing Ids of register to sort 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Register other)
        {
            return this.id.CompareTo(other.id);
        }
    }
    /// <summary>
    /// This Comparator is for comparing the no. of customers in each register which allows to choose
    /// a register for Type A customer
    /// </summary>
    public class CompareRegisterItems : IComparer<Register>
    {
        public int Compare(Register r1,Register r2)
        {
            int sizeR1 = r1.getCustomers().Count();
            int sizeR2 = r2.getCustomers().Count();
            return sizeR1.CompareTo(sizeR2);
        }
    }
}
