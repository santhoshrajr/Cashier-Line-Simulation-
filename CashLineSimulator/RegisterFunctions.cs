using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashLineSimulator
{
    /// <summary>
    /// Register Functions class has functions to getShortestRegister by size, index for TypeA Customers.
    /// For Type B customers we use register with least items.
    ///
    /// </summary>
    public class RegisterFunctions
    {
        private List<Register> registerList = new List<Register>();
        public RegisterFunctions(int registers)
        {
            for(int i=0;i<registers;i++)
            {
                registerList.Add(new Register(i));
            }
        }
        /// <summary>
        /// Return List of Registers 
        /// </summary>
        /// <returns></returns>
        public List<Register> getRegisterList()
        {
            return registerList;
        }
        /// <summary>
        /// Returns Register which is shortest by ID
        /// </summary>
        /// <returns></returns>
        public Register getRegisterShortestById()
        {
            List<Register> sortedList = new List<Register>();
            foreach(Register register in registerList)
            {
                sortedList.Add(register);
            }
            sortedList.Sort();
            return sortedList[0];
        }
        /// <summary>
        /// Returns Register Shortest by Size for Type A customer
        /// </summary>
        /// <returns></returns>
        public Register getRegisterShortestBySize()
        {
            List<Register> sortedList = new List<Register>();
            foreach(Register register in registerList)
            {
                sortedList.Add(register);
            }
            sortedList.Sort(new CompareRegisterItems());
            return sortedList[0];
        }
        /// <summary>
        /// Returns Register with least Items for Type B customer
        /// </summary>
        /// <returns></returns>
        public Register getRegisterWithLeastItems()
        {
            Dictionary<Customer, Register> custRegDct = new Dictionary<Customer, Register>();
            List<Register> emptyRegisterList = new List<Register>();
            List<Customer> customerItems = new List<Customer>();
            foreach(Register register in registerList)
            {
                if(register.getCustomers().Count()==0)
                {
                    emptyRegisterList.Add(register);
                }
                else
                {
                    Customer lastcustomer = null;
                   Queue<Customer> customerQueueReg= register.getCustomers();
                    foreach(Customer c in customerQueueReg)
                    {
                        lastcustomer = customerQueueReg.Peek();
                    }
                    custRegDct[lastcustomer] = register;
                    customerItems.Add(lastcustomer);
                }
            }
            if(emptyRegisterList.Count()>0)
            {
                emptyRegisterList.Sort();
                return emptyRegisterList[0];
            }
            else
            {
                customerItems.Sort();
                return custRegDct[customerItems[0]];
            }
        }
        /// <summary>
        /// Service Customer to particular register based on their type
        /// </summary>
        /// <param name="customerlist"></param>
        public void serviceCustomer(List<Customer> customerlist)
        {
            foreach(Customer customer in customerlist)
            {
                if(customer.getType().Equals(Type.A))
                {
                    Register registerShortestsize = getRegisterShortestBySize();
                    registerShortestsize.getCustomers().Enqueue(customer);
                }
                else
                {
                    Register registerGetLeastItems = getRegisterWithLeastItems();
                    registerGetLeastItems.getCustomers().Enqueue(customer);
                }
            }
        }
        /// <summary>
        /// To Check if any of the registers are serving customers.
        /// </summary>
        /// <returns></returns>
        public bool getRegisterstatus()
        {
            foreach(Register register in registerList)
            {
                if(register.getCustomers().Count()!=0)
                {
                    return true;
                }
                
            }
            return false;
        }



    }
}
