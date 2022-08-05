using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using SportsStore.Components;
using SportsStore.Models;
using Xunit;

namespace SportsStore.Tests
{
    public  class NavigationMenuViewComponentTests
    {
        [Fact]
        public void Can_Select_Categories()
        {
            //Организация
            Mock<IStoreRepository> mock =new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product { ProductID = 1, Name = "P1", Category = "Apples"},
                new Product { ProductID = 2, Name = "P2", Category = "Apples"},
                new Product { ProductID = 3, Name = "P3", Category = "Plums"},
                new Product { ProductID = 4, Name = "P4", Category = "Oranges"}
            }).AsQueryable<Product>());
            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);
            //Действие - получение набора категорий
            string[] results = ((IEnumerable<string>)(target.Invoke() as ViewViewComponentResult).ViewData.Model).ToArray();
            //Утвверждение
            Assert.True(Enumerable.SequenceEqual(new string[] { "Apples", "Oranges", "Plums" }, results));
        }
        [Fact]
        public void Indicates_Selected_Category()
        {
            //Организация
            string categoryToSelect = "Apples";
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {ProductID = 1, Name = "P1", Category = "Apples"},
                new Product {ProductID = 4, Name = "P2", Category = "Oranges"}
            }).AsQueryable<Product>());
            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);
            target.ViewComponentContext = new ViewComponentContext { ViewContext = new ViewContext { RouteData = new Microsoft.AspNetCore.Routing.RouteData() } };
            target.RouteData.Values["category"] = categoryToSelect;
            //Действие
            string result = (string)(target.Invoke() as ViewViewComponentResult).ViewData["SelectedCategory"];
            //Утверждение
            Assert.Equal(categoryToSelect, result);
        }
    }
}
