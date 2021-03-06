﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Warehouse.Api.Data;

namespace Warehouse.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        [HttpGet]
        [Route("product/{id}")]
        public dynamic Get(int id)
        {
            using var db = new WarehouseContext();
            var item = db.StockItems
                .SingleOrDefault(o => o.ProductId == id);

            return item;
        }

        [HttpGet]
        [Route("products/{ids}")]
        public IEnumerable<dynamic> Get(string ids)
        {
            using var db = new WarehouseContext();
            var productIds = ids.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();
            var items = db.StockItems
                .Where(status => productIds.Any(id => id == status.ProductId))
                .ToArray();

            return items;
        }
    }
}
