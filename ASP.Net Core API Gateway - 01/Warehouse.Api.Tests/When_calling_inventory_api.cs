﻿using System.Threading.Tasks;
using Newtonsoft.Json;
using ServiceComposer.AspNetCore.Testing;
using Warehouse.Api.Data.Models;
using Xunit;

namespace Warehouse.Api.Tests
{
    public class When_calling_inventory_api
    {
        [Fact]
        public async Task Get_product_should_return_200()
        {
            // Arrange
            var client = new WebApplicationFactoryWithWebHost<Startup>().CreateClient();

            // Act
            var response = await client.GetAsync("/api/inventory/product/1");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Get_product_should_return_expected_StockItem()
        {
            // Arrange
            var expectedStockItem = new StockItem()
            {
                Id = 1,
                ProductId = 1,
                Inventory = 4,
            };
            var client = new WebApplicationFactoryWithWebHost<Startup>().CreateClient();

            // Act
            var response = await client.GetAsync("/api/inventory/product/1");
            var responseString =await response.Content.ReadAsStringAsync();
            var stockItem = JsonConvert.DeserializeObject<StockItem>(responseString);

            // Assert
            Assert.Equal(expectedStockItem.Id, stockItem.Id);
            Assert.Equal(expectedStockItem.Inventory, stockItem.Inventory);
            Assert.Equal(expectedStockItem.ProductId, stockItem.ProductId);
        }
    }
}