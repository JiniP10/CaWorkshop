using System;
using System.Collections.Generic;
using System.Text;

using CaWorkshop.Domain.Common;

namespace CaWorkshop.Domain.Kanban.Exceptions;

public class InvalidTitleException(string message) : DomainException(message)
{
}