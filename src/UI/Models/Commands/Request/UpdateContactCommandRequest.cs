using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseAssessment.UI
{
    public class UpdateContactCommandRequestViewModel 
    {
        public string Id { get; set; }
        [Required]
        public string Email { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
    }
}
