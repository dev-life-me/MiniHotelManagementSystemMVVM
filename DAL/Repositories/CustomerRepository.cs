using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository(FuminiHotelManagementContext context) : base(context)
        {
        }

        public Customer GetByEmailAndPassword(string email, string password)
        {
            return _dbSet.FirstOrDefault(cus => cus.EmailAddress == email && cus.Password == password);
        }

        public Customer GetCustomerById(int id)
        {
           return _dbSet.FirstOrDefault(cus => cus.CustomerId == id);
        }
    }
}
