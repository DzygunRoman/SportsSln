using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using Xunit;

namespace SportsStore.Tests
{
    public class OrderControllerTests
    {
        [Fact]
        public void Cannot_Checkout_Empty_Cart()
        {
            //Организация - создание имитированного хранилища заказов
            Mock<IOrderRepository>mock = new Mock<IOrderRepository>();
            //Организция - создание пустой корзины
            Cart cart = new Cart();
            //Организация - создание заказа
            Order order = new Order();
            //Организация - создание экземпляра контроллера
            OrderController target = new OrderController(mock.Object,cart);
            //Действие
            ViewResult result = target.Checkout(order) as ViewResult;
            //Утверждение - проверка, что заказ не был сохранен
            mock.Verify(m=>m.SaveOrder(It.IsAny<Order>()), Times.Never);
            //Утверждение - проверка, что метод возвращает стандартное представление
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            //Утверждение - проверка, что представлению передана недопустимая модель
            Assert.False(result.ViewData.ModelState.IsValid);
        }
    }
}
