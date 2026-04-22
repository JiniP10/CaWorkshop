using System;
using System.Collections.Generic;
using System.Text;

using CaWorkshop.Domain.Common;

namespace CaWorkshop.Domain.Kanban.Exceptions;

public class InvalidCardPositionException : DomainException
{
    public InvalidCardPositionException(int position)
        : base($"Card position '{position}' is out of range.")
    {
    }
}
