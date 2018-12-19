using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Models
{
    public class SessionSynopsis
    {
        public int SessionSynopsisId { get; set; }
        public string SessionSynopsisName { get; set; }

        public int CreatedById { get; set; }
        public User CreatedBy { get; set; }
        public int UpdatedById { get; set; }
        public User UpdatedBy { get; set; }
        //IsVisible shares similar meaning as IsEnabled in the AccountDetail domain class definition.
        public bool IsVisible { get; set; }

    }
}
