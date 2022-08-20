using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.Dto_s
{
    public class CreateChatDto
    {
        public string UserA { get; set; }
        public string UserB { get; set; }

        public CreateChatDto(string UserAId, string UserBId)
        {
            UserA = UserAId;
            UserB = UserBId;
        }
    }


}
