﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMVM_BLL.DTO
{
    public class CreateClientDTO
    {

        public string Name { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }
    }
}
