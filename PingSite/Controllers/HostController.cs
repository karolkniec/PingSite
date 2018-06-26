﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PingSite.Core.Services;
using PingSite.Models.Host;

namespace PingSite.Controllers
{
    public class HostController : Controller
    {
        private readonly IHostService _hostService;
        private readonly ICategoryService _categoryService;

        public HostController(IHostService hostService, ICategoryService categoryService)
        {
            _hostService = hostService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var host = await _hostService.GetAsync(id);

            return View(host);
        }

        [HttpGet]
        public async Task<IActionResult> Add(int id)
        {
            var categories = await _categoryService.GetAllAsync();
            var addHost = new AddHost
            {
                RoomId = id,
                Categories = categories
            };

            return View(addHost);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddHost addHost)
        {
            var status = await _hostService.AddAsync(addHost.Name, addHost.Address, addHost.RoomId, addHost.CategoryId);

            return RedirectToAction("Hosts", "Home", new { id = addHost.RoomId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, int roomId, bool allHosts = false)
        {
            var host = await _hostService.GetAsync(id);
            EditHost editHost = new EditHost
            {
                Id = id,
                Name = host.Name,
                Address = host.Address,
                CategoryId = (int)host.Category.Id,
                RoomId = roomId,
                AllHosts = allHosts
            };
            var categories = await _categoryService.GetAllAsync();
            foreach(var category in categories)
            {
                editHost.Categories.Add(new SelectListItem
                {
                    Text = category.Name,
                    Value = category.Id.ToString()
                });
            }

            return View(editHost);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditHost editHost)
        {
            var status = await _hostService.EditAsync(editHost.Id, editHost.Name, editHost.Address, editHost.RoomId, editHost.CategoryId);

            if(editHost.AllHosts)
            {
                return RedirectToAction("AllHosts", "Home");
            }

            return RedirectToAction("Hosts", "Home", new { id = editHost.RoomId });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id, int roomId)
        {
            var status = await _hostService.RemoveAsync(id);

            if(roomId == 0)
            {
                return RedirectToAction("AllHosts", "Home");
            }
            else
            {
                return RedirectToAction("Hosts", "Home", new { id = roomId });
            }
        }
    }
}
