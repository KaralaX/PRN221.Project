using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.Project.Domain.Entities
{
    public class PaymentInformationModel
    {
        public string OrderType { get; set; }
        public double Amount { get; set; }
        public string OrderDescription { get; set; }
        public string Name { get; set; }
        public virtual Doctor doctor { get; set; }
        public virtual Patient patient { get; set; }
        public virtual ICollection<Service> Services { get; set; }
        public virtual Appointment Appointment { get; set; }
    }
}
