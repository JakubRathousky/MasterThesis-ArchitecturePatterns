using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResSys.Logistic.Application.ViewModels;

namespace ResSys.Logistic.Application.Interfaces
{
    /// <summary>
    /// Service for stock supplies
    /// </summary>
    public interface IStockSuppliesService
    {
        Task<IEnumerable<SupplyDto>> GetAllAsync();
        Task Synchronize();
        Task<SupplyDto> GetByIdAsync(Guid id);

        Task<SupplyDto> CreateAsync(CreateSupplyDto createSupplyDto);

        Task DeleteAsync(Guid id);
    }
}