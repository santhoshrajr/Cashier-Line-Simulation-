using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashLineSimulator
{
    /// <summary>
    /// Customer Class has details of customer type, timeArrived at Register, No of Items
    /// For a customer with Training register we change service status at every alternate 
    /// change of time. We use a comparator to compare no of items between two customers
    /// and if they are same we check their types. This is for customers who arrive at
    /// same time.
    /// </summary>
   public class Customer:IComparable<Customer>
    {
        private Type typeOfCustomer;
        private int timeArrived;
        private int items;
        private bool customerServiceStatus;
        public Customer(Type typeOfCustomer,int timeArrived,int items)
        {
            this.typeOfCustomer = typeOfCustomer;
            this.timeArrived = timeArrived;
            this.items = items;
        }
        
        public int getItemCount()
        {
            return items;
        }

        public void setItemCount(int items)
        {
            this.items = items;
        }

        public int getTimeArrived()
        {
            return timeArrived;
        }

        public void setTimeArrived(int timeArrived)
        {
            this.timeArrived = timeArrived;
        }

        public Type getType()
        {
            return typeOfCustomer;
        }

        public void setType(Type typeOfCustomer)
        {
            this.typeOfCustomer = typeOfCustomer;
        }

        public int removeItems()
        {
            return --this.items;
        }

        public bool getServiceStatus()
        {
            return customerServiceStatus;
        }

        public void setServiceStatus(bool customerServiceStatus)
        {
            this.customerServiceStatus = customerServiceStatus;
        }
        /// <summary>
        /// Implementing CompareTo method to compare items of both customers.
        /// If Same compares their types returns output of comparison which 
        /// helps to sort.
        /// </summary>
        /// <param name="otherCustomer"></param>
        /// <returns></returns>
        public int CompareTo(Customer otherCustomer)
        {
            int result = 0;
            result = this.items.CompareTo(otherCustomer.items);
            if(result==0)
            {
                result = this.typeOfCustomer.CompareTo(otherCustomer.typeOfCustomer);
            }
            return result;
            
        }
    }

    public enum Type
    {
        A,B
    };
}
