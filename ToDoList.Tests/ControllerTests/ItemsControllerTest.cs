using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Controllers;
using System.Collections.Generic;
using Moq;
using System.Linq;

namespace ToDoList.Tests
{
	public class ItemsControllerTest
	{
		Mock<IItemRepository> mock = new Mock<IItemRepository>();

        private void DbSetup()
        {
			mock.Setup(m => m.Items).Returns(new Item[]
			{
				new Item(){ItemId = 1, Description = "Drench the dog", CategoryId = 1},
				new Item(){ItemId = 2, Description = "Clean the cat", CategoryId = 1},
				new Item(){ItemId = 3, Description = "Scrub the squirrel", CategoryId = 1}
			}.AsQueryable());
        }
		[TestMethod]
        public void GetDescriptionTest()
        {
			ItemsController controller = new ItemsController();

			var result = controller.Index();

            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        [TestMethod]
        public void Get_ModelList_Index_Test()
        {
            //Arrange
            DbSetup();
            ItemsController controller = new ItemsController(mock.Object);

            //Act
            var result = controller.ViewData.Model;

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<Item>));
        }

        [TestMethod]
        public void Post_MethodAddsItem_Test()
        {
            //Arrange
            DbSetup();
            ViewResult indexView = new ItemsController(mock.Object).Index() as ViewResult;

            //Act
            controller.Create(testItem);
            ViewResult indexView = new ItemsController().Index() as ViewResult;
            var collection = indexView.ViewData.Model as List<Item>;

            //Assert
            CollectionAssert.Contains(collection, testItem);
        }


	}
}