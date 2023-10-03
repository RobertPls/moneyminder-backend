using Infrastructure.EntityFramework.Context;
using MediatR;
using SharedKernel.Core;

namespace Infrastructure.EntityFramework
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly WriteDbContext _context;
        private readonly IMediator _mediator;

        public UnitOfWork(WriteDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task Commit()
        {
            var domainEvents = _context.ChangeTracker.Entries<Entity<Guid>>()
                .Select(x => x.Entity.DomainEvents)
                .SelectMany(x => x)
                .Where(x => !x.Consumed)
                .ToArray();

            foreach (var domainEvent in domainEvents)
            {
                domainEvent.MarkAsConsumed();
                await _mediator.Publish(domainEvent);
            }
            await _context.SaveChangesAsync();
        }
    }
}
