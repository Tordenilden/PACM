using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PACM.Classes
{
    public class Customer : EntityBase
    {
        #region Interface
        public string log() => $"{customerId}:{name} Email: {email}";
        #endregion Interface

        #region properties
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public int customerId { get; private set; }
        public string name
        {
            get
            {
                string fullName = lastname;
                if (!string.IsNullOrWhiteSpace(firstname))
                {
                    if (!string.IsNullOrWhiteSpace(fullName))
                    {
                        fullName += ",";
                    }
                    fullName += firstname;
                }
                return fullName;
            }
        }
        public static int instanceCount { get; set; }

        // NEW Composition
        //public List<Address> addressList { get; set; }

        public int customerType { get; set; } // Inheritance or these


        #endregion properties
        // constructor chaining
        public Customer() : this(0) { }
        public Customer(int _customerId) { customerId = _customerId;  }
        #region methods

        #endregion methods

        #region Leverage
        public override bool validate()
        {
            var isValid = true;
            if (string.IsNullOrWhiteSpace(lastname)) isValid = false;
            if (string.IsNullOrWhiteSpace(email)) isValid = false;
            return isValid;
        }
        #endregion Leverage


    }
}
