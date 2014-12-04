using System;

namespace RepositoryUnitOfWorkPatterns.Models
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}