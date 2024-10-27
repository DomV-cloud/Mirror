﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirror.Contracts.Authentication
{
    /// <summary>
    /// Record - is good to use for readonly use-cases, like data tranfer (DTO)
    /// </summary>
    public record RegisterRequest
    (
        string FirstName,
        string LastName,
        string Email,
        string Password
    );
}
