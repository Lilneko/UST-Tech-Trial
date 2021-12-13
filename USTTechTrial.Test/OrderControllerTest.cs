using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using USTTechTrial.Context;
using USTTechTrial.Controllers;
using USTTechTrial.Models;
using Xunit;

namespace USTTechTrial.Test
{
    public class OrderControllerTest
    {
        OrderController _controller;

        public OrderControllerTest(OrderContext context)
        {
            _controller = new OrderController(context);

        }



        [Fact]
        public void AddOrderTest()
        {
            //OK RESULT TEST START

            //Arrange
            var completeOrder = new Order()
            {
                orderId = "NewOrder",
                items = new List<Item>() {
                    new Item(){ code = "ItemA1", units = 6, pricePerUnit = 2.5M, type = 1 },
                    new Item(){ code = "ItemA2", units = 12, pricePerUnit = 5, type = 2 },
                }
            };

            //Act
            var createdResponse = _controller.PostOrder(completeOrder);

            //Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse.Result);

            //value of the result
            var item = createdResponse.Result.Result as CreatedAtActionResult;
            Assert.IsType<Order>(item.Value);

            //check value of this book
            var OrderItem = item.Value as Order;
            foreach (Item it in OrderItem.items ?? new List<Item>())
            {
                Assert.NotNull(it.subTotal);
                Assert.NotNull(it.vatPercentage);
                Assert.NotNull(it.totalWithVat);
            };
            Assert.NotNull(OrderItem.total);

             //OK RESULT TEST END






             //BADREQUEST AND MODELSTATE ERROR TEST START

             //Arrange
            // var incompleteBook = new Order()
            //{
            //    Author = "Author",
            //    Description = "Description"
            //};

            ////Act
            //_controller.ModelState.AddModelError("Title", "Title is a requried filed");
            //var badResponse = _controller.Order(incompleteBook);

            ////Assert
            //Assert.IsType<BadRequestObjectResult>(badResponse);



            //BADREQUEST AND MODELSTATE ERROR TEST END
        }
    }
}
