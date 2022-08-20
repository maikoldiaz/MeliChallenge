using System;
namespace Meli.DataAccess.Interfaces
{
    /// <summary>
    /// The unit of work factory.
    /// </summary>
    public interface IUnitOfWorkFactry
    {
        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        /// <returns>The unit of work.</returns>
        IUnitOfWork GetUnitOfWork();
    }
}

