using System;
using Meli.DataAccess.Interfaces;

namespace Meli.DataAccess
{
    public class UnitOfWorkFactory: IUnitOfWorkFactory
    {
        /// <summary>
        /// The data context.
        /// </summary>
        private readonly IDataContext dataContext;

        /// <summary>
        /// The repository factory.
        /// </summary>
        private readonly IRepositoryFactory repositoryFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWorkFactory" /> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <param name="repositoryFactory">The repository factory.</param>
        public UnitOfWorkFactory(IDataContext dataContext, IRepositoryFactory repositoryFactory)
        {
            this.dataContext = dataContext;
            this.repositoryFactory = repositoryFactory;
        }

        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        /// <returns>
        /// The unit of work.
        /// </returns>
        public IUnitOfWork GetUnitOfWork()
        {
            return new UnitOfWork(this.dataContext, this.repositoryFactory);
        }
    }
}

