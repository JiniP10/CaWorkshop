using System;
using System.Collections.Generic;
using System.Text;

using CaWorkshop.Domain.Common;

namespace CaWorkshop.Domain.Kanban.Events;

public record CardMovedEvent(Guid CardId, Guid FromColumnId, Guid ToColumnId) : IDomainEvent;
