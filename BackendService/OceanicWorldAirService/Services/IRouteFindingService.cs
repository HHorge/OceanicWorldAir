﻿namespace OceanicWorldAirService.Services
{
    /// <summary>
    /// RouteFindingService interface.
    /// </summary>
    public interface IRouteFindingService
    {
        /// <summary>
        /// Method for retrieving the calculated Routes.
        /// </summary>
        public Task<IEnumerable<int>> FindRoutes();
    }
}