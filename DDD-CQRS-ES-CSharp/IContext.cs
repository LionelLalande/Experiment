using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DDD_CQRS_ES_CSharp
{
    [DebuggerDisplay("{ToString(),nq}")]
    public struct Revision
    {
        private Revision(long l)
        {
            _value = l;
        }

        public override string ToString() => _value.ToString();

        public static Revision operator +(Revision a, long b) => new Revision(a._value + b);

        public static implicit operator Revision(long s) => new Revision(s);

        private readonly long _value;
    }
    public sealed class AggregateRoot<TIdentifier, TEntity>
    {
        public TIdentifier Id { get => _id; }

        public Revision Rev { get => _rev; }

        public TEntity Value { get => _root; }

        public AggregateRoot(TIdentifier id, Revision rev, TEntity root)
        {
            _id = id;
            _rev = rev;
            _root = root;
        }

        public void NextRevision(TEntity value)
        {
            _rev = _rev + 1;
            _root = value;
        }

        private readonly TIdentifier _id;
        private Revision _rev;
        private TEntity _root;
    }
    public static class AggregateRoot
    {
        ////public static AggregateRoot<TIdentifier, TEntity> Apply<TIdentifier, TEntity>(TEntity root) => Apply(new TIdentifier(), root);

        public static AggregateRoot<TIdentifier, TEntity> Apply<TIdentifier, TEntity>(TIdentifier id, TEntity root) => new AggregateRoot<TIdentifier, TEntity>(id, -1, root);
    }

    public interface IContext<TIdentifier, TEntity, TCommand, TEvent>
    {
        IRepository<TIdentifier, TEntity> Repository { get; }

        TEntity ApplyEvent(TEntity value, TEvent @event);

        TEntity CreateNew();

        TIdentifier CreateNewId();

        ////TEvent ToEvent(TCommand command);

        Func<TCommand, TEvent> Exec(TEntity entity);
    }
    public interface IRepository<TIdentifier, TEntity>
    {
        void Save(AggregateRoot<TIdentifier, TEntity> aggregateRoot);

        AggregateRoot<TIdentifier, TEntity> Get(TIdentifier aggregateId);
    }
    public sealed class Event<TIdentifier, TEvent> //: Event
    {
        public TIdentifier Id { get => _aggregateid; }

        public TEvent Value { get => _value; }

        public Event(TIdentifier aggregateId, TEvent value)
        {
            _aggregateid = aggregateId;
            _value = value;
        }

        private readonly TIdentifier _aggregateid;
        private readonly TEvent _value;
    }
    public interface IEventStore<TIdentifier, TEvent>
    {
        void Add(Event<TIdentifier, TEvent> @event);

        IReadOnlyCollection<Event<TIdentifier, TEvent>> GetHistory(TIdentifier aggregateId);
    }
    public interface IStateStore<in TIdentifier, TState>
    {
        TState Load(TIdentifier aggregateId);

        void Save(TIdentifier aggregateId, TState state);
    }

    public class InMemoryEventStore<TIdentifier, TEvent> : IEventStore<TIdentifier, TEvent>
    {
        public void Add(Event<TIdentifier, TEvent> @event)
        {
            Debug.WriteLine(@event.Value.GetType());
            _events.Add(@event);
        }

        public IReadOnlyCollection<Event<TIdentifier, TEvent>> GetHistory(TIdentifier aggregateId)
        {
            return _events.OfType<Event<TIdentifier, TEvent>>().Where(e => e.Id.Equals(aggregateId)).ToList();
        }

        private static readonly List<object> _events = new List<object>();
    }
    public class InMemoryStateStore<TIdentifier, TState> : IStateStore<TIdentifier, TState>
    {
        public TState Load(TIdentifier aggregateId)
        {
            _states.TryGetValue(aggregateId, out var state);
            return state;
        }

        public void Save(TIdentifier aggregateId, TState state)
        {
            _states[aggregateId] = state;
        }

        private static readonly Dictionary<TIdentifier, TState> _states = new Dictionary<TIdentifier, TState>();
    }

    public sealed class EventStoreRepository<TIdentifier, TEntity, TCommand, TEvent> : IRepository<TIdentifier, TEntity>
    {
        public EventStoreRepository(IEventStore<TIdentifier, TEvent> eventStore, IContext<TIdentifier, TEntity, TCommand, TEvent> context)
        {
            _eventStore = eventStore;
            _context = context;
        }

        public AggregateRoot<TIdentifier, TEntity> Get(TIdentifier aggregateId)
        {
            if (!_values.TryGetValue(aggregateId, out var ar))
            {
                var events = _eventStore.GetHistory(aggregateId);
                if (events.Count == 0) return null;
                ar = new AggregateRoot<TIdentifier, TEntity>(aggregateId, -1, _context.CreateNew());
                foreach (var e in events)
                {
                    ar.NextRevision(_context.ApplyEvent(ar.Value, e.Value));
                }
                _values.Add(aggregateId, ar);
            }
            return ar;
        }

        public void Save(AggregateRoot<TIdentifier, TEntity> aggregateRoot)
        {

            _values[aggregateRoot.Id] = aggregateRoot;
        }

        private readonly static Dictionary<TIdentifier, AggregateRoot<TIdentifier, TEntity>> _values = new Dictionary<TIdentifier, AggregateRoot<TIdentifier, TEntity>>();
        private readonly IEventStore<TIdentifier, TEvent> _eventStore;
        private readonly IContext<TIdentifier, TEntity, TCommand, TEvent> _context;
    }
    public class StateStoreRepository<TIdentifier, TEntity> : IRepository<TIdentifier, TEntity>
    {
        public StateStoreRepository(IStateStore<TIdentifier, TEntity> stateStore)
        {
            _stateStore = stateStore;
        }

        public AggregateRoot<TIdentifier, TEntity> Get(TIdentifier aggregateId)
        {
            if (!_values.TryGetValue(aggregateId, out var ar))
            {
                var state = _stateStore.Load(aggregateId);
                ar = new AggregateRoot<TIdentifier, TEntity>(aggregateId, -1, state);
                _values.Add(aggregateId, ar);
            }
            return ar;
        }

        public void Save(AggregateRoot<TIdentifier, TEntity> aggregateRoot)
        {
            _values[aggregateRoot.Id] = aggregateRoot;
            _stateStore.Save(aggregateRoot.Id, aggregateRoot.Value);
        }

        private readonly static Dictionary<TIdentifier, AggregateRoot<TIdentifier, TEntity>> _values = new Dictionary<TIdentifier, AggregateRoot<TIdentifier, TEntity>>();
        private readonly IStateStore<TIdentifier, TEntity> _stateStore;
    }

    public interface ICommand<out TIdentifier, out TCommand>
    {
        TIdentifier Id { get; }

        TCommand Value { get; }
    }
    public sealed class Command<TIdentifier, TCommand> : ICommand<TIdentifier, TCommand>
    {
        public TIdentifier Id { get; }

        public TCommand Value { get; }

        public Command(TIdentifier id, TCommand value)
        {
            Id = id;
            Value = value;
        }
    }
    public class CommandHandler<TIdentifier, TEntity, TCommand, TEvent>
    {
        public CommandHandler(IContext<TIdentifier, TEntity, TCommand, TEvent> context)
        {
            _context = context;
            _repository = context.Repository;
        }

        public AggregateRoot<TIdentifier, TEntity> Apply(ICommand<TIdentifier, TCommand> command)
        {
            var aggregateId = command.Id;

            var ar = _repository.Get(aggregateId) ?? AggregateRoot.Apply<TIdentifier, TEntity>(_context.CreateNewId(), _context.CreateNew());

            var @event = _context.Exec(ar.Value)(command.Value);
            ////eventStore.Add(new Event<TIdentifier, TEvent>(aggregateId, @event)); // TODO

            ar.NextRevision(_context.ApplyEvent(ar.Value, @event));

            _repository.Save(ar);

            return ar;
        }

        ////private AggregateRoot<TIdentifier, TEntity> Handle(EventStoreRepository<TIdentifier, TEvent, TCommand, TEvent> eventStore, ICommand<TIdentifier, TCommand> command)
        ////{

        ////}

        ////private AggregateRoot<TIdentifier, TEntity> Handle(StateStoreRepository<TIdentifier, TEntity> stateStore, ICommand<TIdentifier, TCommand> command)
        ////{
        ////    var aggregateId = command.Id;

        ////    var state = _repository.Load(aggregateId);
        ////    if (state == null) return null;

        ////    var ar = _repository.Get(aggregateId) ?? AggregateRoot.Apply<TIdentifier, TEntity>(state);

        ////    var @event = _context.Exec(ar.Value)(command.Value);
        ////    ar.NextRevision(_context.ApplyEvent(ar.Value, @event));

        ////    _repository.Save(ar);

        ////    return ar;
        ////}

        private readonly IContext<TIdentifier, TEntity, TCommand, TEvent> _context;
        private readonly IRepository<TIdentifier, TEntity> _repository;
    }
}
