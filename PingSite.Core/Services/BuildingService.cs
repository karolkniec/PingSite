﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PingSite.Core.DTO;
using PingSite.Core.Models;
using PingSite.Core.Repositories;

namespace PingSite.Core.Services
{
    public class BuildingService : IBuildingService
    {
        private readonly IBuildingRepository _buildingRepository;

        public BuildingService(IBuildingRepository buildingRepository)
        {
            _buildingRepository = buildingRepository;
        }

        public async Task<IEnumerable<BuildingDto>> GetAllAsync()
        {
            var buildings = await _buildingRepository.GetAllAsync();
            var buildingsDto = new List<BuildingDto>();

            foreach(var b in buildings)
            {
                buildingsDto.Add(new BuildingDto() { Id = b.Id, Name = b.Name });
            }

            return buildingsDto;
        }

        public async Task<bool> AddAsync(string name)
        {
            var building = Building.Create(null, name);
            await _buildingRepository.AddAsync(building);

            return true;
        }

        public async Task<bool> UpdateAsync(int id, string name)
        {
            var building = await _buildingRepository.GetAsync(id);
            if(building == null)
            {
                return false;
            }
            building.SetName(name);
            await _buildingRepository.UpdateAsync(building);

            return true;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var building = await _buildingRepository.GetAsync(id);
            if(building == null)
            {
                return false;
            }

            await _buildingRepository.RemoveAsync(building);

            return true;
        }
    }
}
