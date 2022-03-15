using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PACM.Classes
{
    public class CustomerRepository
    {
        //new Composition
        // Definition, Declaration, oprettelse (create)
        public string myVariable { get; set; }
        public List<Customer> customerList { get; set; } 
        public CustomerRepository()
        {
            //new
            //addressRepository = new AddressRepository();
            customerList = new List<Customer>
            {
                new Customer(1)
                {
                    firstname="Bo",
                    lastname="Nielsen",
                    email="hat@hat.dk",
                    //Collaborative relationship
                    //addressList= addressRepository.retrieveByCustomerId(1).ToList()
                },
                new Customer(2)
                {
                    firstname="Ib",
                    lastname="Nielsen",
                    email="kop@kop.dk",
                    //Collaborative relationship
                    //addressList= addressRepository.retrieveByCustomerId(2).ToList()
                }
            };


        }
        #region methods
       
        //Collaboration parameteren + type ofc
        public bool save(Customer customer)
        {

            return true;
        }

        #endregion methods

        public Customer retrieve(int customerId)
        {
            //Collaboration
            Customer customer = new Customer(customerId)
            {
                firstname = "Bo",
                lastname = "Nielsen",
                email = "hat@hat.dk",
                //Collaborative relationship
            };
            return customer;
        }
        string connectionString = "Data Source=TEC-5350-LA0052;Database=pacm3;Integrated Security=false;TrustServerCertificate=True;user id =xxy; password=Flemming";
        //string connectionString = "Data Source=TEC-5350-LA0052;Initial Catalog=pac3;Integrated Security=True;";
        // DATABASE  - TODO
        // Script?
        // Dummy data
        // Without stored procedures
        // With Stored
        #region version 2 getcustomer
        public Customer retrieveDB(int customerId)
        {
            Customer customer = new Customer();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //string query = $"select * from customer where customerId=1";
                string query2 = "1";
                string query = $"select * from customer where customerId={customerId}";
                //string query = $"select * from customer where customerId="+query2;
                //int tal = 4;
                //SqlCommand cmd1 = new SqlCommand("select * from customer where customerId=1", connection);
                SqlCommand cmd2 = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = cmd2.ExecuteReader();
                reader.Read(); // tjekker om der er data i reader og return bool hvis der er
                // det der sker er den "læser 1 linje i et array og sætter markøren klar til at læse
                // næste linje fra arrayet
                //customer.firstname = "bo";
                customer.firstname = reader["firstname"].ToString();
                customer.lastname = reader["lastname"].ToString();
                //customer.customerId = Convert.ToInt32(reader["customerId"]);
                //.....
                connection.Close();
            }
                return customer;
        }
        #endregion version 2 getcustomer


        #region version 2 getcustomers
        public List<Customer> retrieveCustomers()
        {
            List<Customer> customerList = new List<Customer>();
            Customer customer = new Customer();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "select * from customer";
                SqlCommand cmd2 = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = cmd2.ExecuteReader();
                // Hvad skal jeg gøre??!!!
                while (reader.Read())
                {
                    customer.firstname = reader["firstname"].ToString();
                    customer.lastname = reader["lastname"].ToString();
                    // her vil jeg gerne have mit objekt ind i min liste!!
                    // hvordan gør jeg det?
                    customerList.Add(customer);
                }
                connection.Close();
            }
            return customerList;
        }
        #endregion version 2 getcustomers
        #region version 2 getcustomer
        public Customer retrieveProc(Customer obj)
        {
            string query = "usp_readCustomerById";
            //usp_readCustomerById
            Customer customer = new Customer();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //SqlCommand cmd = new SqlCommand("vores storedprocedures navn", connection);
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("sqlscript", c# parameter );
                cmd.Parameters.AddWithValue("@customerId", obj.customerId);

                // talk to our server
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                customer.firstname = reader["firstname"].ToString();
                customer.lastname = reader["lastname"].ToString();
            }
            return customer;
        }
        #endregion version 2 getcustomer
    }
}
