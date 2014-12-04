using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CTPWebApi.Models.InputDtos
{
    public class ObjectOrderingDto
    {
        public Guid ObjectId { get; set; }
        public int Order { get; set; }

    }
}