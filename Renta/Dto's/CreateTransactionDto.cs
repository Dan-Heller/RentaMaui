using Renta.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.Dto_s
{
    public class CreateTransactionDto
    {
        
        public string? ItemOwner { get; set; }

        
        public string? ItemSeeker { get; set; }

  
        public string? ItemId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? CreatedAt { get; set; } = new DateTime();

        public ETransactionStatus Status { get; set; } = ETransactionStatus.Pending;

    }
}
