using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFactoryServiceDAL.ViewM
{
    public class CustomerViewM
    {
        public int Id { get; set; }
        [DisplayName("ФИО Клиента")]
        public string CustomerFIO { get; set; } //ClientFIO
    }
}
