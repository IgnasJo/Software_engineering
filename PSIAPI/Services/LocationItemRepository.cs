﻿using Microsoft.EntityFrameworkCore;
using PSIAPI.Interfaces;
using PSIAPI.Models;
using PSIAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace PSIAPI.Services
{
    public class LocationItemRepository : ILocationItemRepository
    {
        private readonly AppDbContext _context;

        public LocationItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(string id)
        {
            var exists = await _context.LocationItems.AnyAsync(t => t.ID.Equals(id));
            return exists;
        }

        public async Task<LocationItem?> FindAsync(string id)
        {
            var locationItemModel = await _context.LocationItems.FirstOrDefaultAsync(t => t.ID.Equals(id));
            return locationItemModel;
        }

        public async Task DeleteAsync(string id)
        {
            _context.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(LocationItem item)
        {
            await _context.LocationItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<List<LocationItem>> GetAllAsync()
        {
            var items = await _context.LocationItems.ToListAsync();
            return items;
        }

        public async Task UpdateAsync(LocationItem existingItem, LocationItem item)
        {
            existingItem.State = item.State;
            existingItem.City = item.City;
            existingItem.Street = item.Street;
            existingItem.Longitude = item.Longitude;
            existingItem.Latitude = item.Latitude;
            await _context.SaveChangesAsync();
        }
    }
}
