using System;
using System.Collections.Generic;
using System.Text;

using MediatR;

using CaWorkshop.Domain.Kanban.Events;

namespace CaWorkshop.Application.Kanban.Events.CardMoved;

public class CardMovedEventHandler : INotificationHandler<CardMovedEvent>
{
    public Task Handle(CardMovedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Card {notification.CardId} moved from column {notification.FromColumnId} to {notification.ToColumnId}.");

        return Task.CompletedTask;
    }
}