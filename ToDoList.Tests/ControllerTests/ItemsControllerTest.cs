using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Controllers;
using Moq;
using System.Linq;

namespace ToDoList.Tests.ControllerTests
{

	[TestClass]
	public class ItemsControllerTests
	{
		Mock<IItemRepository> mock = new Mock<IItemRepository>();
        EFItemRepository db = new EFItemRepository(new TestDbContext());

		private void DbSetup()
		{
			mock.Setup(m => m.Items).Returns(new Item[]
			{
				new Item {ItemId = 1, Description = "Wash the dog", CategoryId=1 },
				new Item {ItemId = 2, Description = "Do the dishes", CategoryId=1 },
				new Item {ItemId = 3, Description = "Sweep the floor", CategoryId=1 }
			}.AsQueryable());
		}

		[TestMethod]
		public void Mock_GetViewResultIndex_Test() //Confirms route returns view
		{
			//Arrange
			DbSetup();
			ItemsController controller = new ItemsController(mock.Object);

			//Act
			var result = controller.Index();

			//Assert
			Assert.IsInstanceOfType(result, typeof(ActionResult));
		}

		[TestMethod]
		public void Mock_IndexListOfItems_Test() //Confirms model as list of items
		{
			// Arrange
			DbSetup();
			ViewResult indexView = new ItemsController(mock.Object).Index() as ViewResult;

			// Act
			var result = indexView.ViewData.Model;

			// Assert
			Assert.IsInstanceOfType(result, typeof(List<Item>));
		}

		[TestMethod]
		public void Mock_ConfirmEntry_Test() //Confirms presence of known entry
		{
			// Arrange
			DbSetup();
			ItemsController controller = new ItemsController(mock.Object);
			Item testItem = new Item();
			testItem.Description = "Wash the dog";
			testItem.ItemId = 1;
			testItem.CategoryId = 1;

			// Act
			ViewResult indexView = controller.Index() as ViewResult;
			var collection = indexView.ViewData.Model as List<Item>;

			// Assert
			CollectionAssert.Contains(collection, testItem);
		}

        [TestMethod]
        public void DB_CreateNewEntry_Test()
        {
            //Arrange
            ItemsController controller = new ItemsController(db);
            Item testItem = new Item();
			testItem.Description = "TestDb Item";
			testItem.CategoryId = 1;

            //Act
            controller.Create(testItem);
            var collection = (controller.Index() as ViewResult).ViewData.Model as List<Item>;

            //Assert
            CollectionAssert.Contains(collection, testItem);
        }

	}
}