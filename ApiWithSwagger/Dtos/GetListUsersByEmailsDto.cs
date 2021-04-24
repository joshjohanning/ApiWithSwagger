using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithSwagger.Dtos
{
    public class GetListUsersByEmailsDto
    {
        public List<string> Emails { get; set; }
    }
}
