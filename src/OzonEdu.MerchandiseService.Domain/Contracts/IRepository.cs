using System.Threading;
using System.Threading.Tasks;

namespace OzonEdu.MerchandiseService.Domain.Contracts
{
    /// <summary>
    ///     Базовый интерфейс репозитория.
    /// </summary>
    /// <typeparam name="TAggregationRoot">Объект сущности для управления</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        ///     Объект <see cref="IUnitOfWork"/>.
        /// </summary>
        IUnitOfWork UnitOfWork { get; }
        
        /// <summary>
        ///     Создать новую сущность.
        /// </summary>
        /// <param name="itemToCreate">Объект для создания</param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Созданная сущность</returns>
        Task<T> CreateAsync(T itemToCreate, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Обновить существующую сущность.
        /// </summary>
        /// <param name="itemToUpdate">Объект для создания.</param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Обновленная сущность сущность</returns>
        Task<T> UpdateAsync(T itemToUpdate, CancellationToken cancellationToken = default);
    }
}