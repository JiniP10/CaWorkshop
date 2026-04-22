using System;
using System.Collections.Generic;
using System.Text;

namespace CaWorkshop.Domain.Kanban.Entities;

using Ardalis.GuardClauses;

using CaWorkshop.Domain.Common;
using CaWorkshop.Domain.Kanban.Guards;
using CaWorkshop.Domain.Kanban.ValueObjects;

public class Column : Entity
{
    public Guid BoardId { get; private set; }

    public ColumnTitle Title { get; private set; }

    public int Position { get; private set; }

    private readonly List<Card> _cards = [];
    public IReadOnlyCollection<Card> Cards => _cards;

    internal Column(Guid boardId, ColumnTitle title, int position)
    {
        BoardId = boardId;
        Title = Guard.Against.Null(title);
        Position = position;
    }

    public Card GetCardById(Guid id)
    {
        var card = _cards.SingleOrDefault(c => c.Id == id);

        Guard.Against.NotFound(id, card);

        return card;
    }

    public Card AddCard(string title, string? description = null)
    {
        var cardTitle = CardTitle.Create(title);

        var position = _cards.Count;

        var card = new Card(columnId: Id, cardTitle, description, position);

        _cards.Add(card);

        return card;
    }

    public void InsertCard(int position, Card card)
    {
        Guard.Against.InvalidCardPosition(position, _cards.Count);

        _cards.Insert(position, card);

        RepositionCards();
    }

    public Card RemoveCard(Guid cardId)
    {
        var card = GetCardById(cardId);

        _cards.Remove(card);

        RepositionCards();

        return card;
    }

    private void RepositionCards()
    {
        for (int i = 0; i < _cards.Count; i++)
        {
            _cards[i].UpdatePosition(i);
        }
    }
}
