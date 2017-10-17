using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Models;

namespace ToDoList.Tests
{
	[TestClass]
	public class ItemTest
	{
		[TestMethod]
		public void GetDescriptionTest()
		{
			//Arrange
			var item = new Item();

			//Act
			var result = item.Description = "Wash the dog";

			//Assert
			Assert.AreEqual("Wash the dog", result);
		}
	}
}