using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Models
{
	[Table("ITEMS")]
	public class Item
	{
		[Key]
		public int ItemId { get; set; }
		public string Description { get; set; }
        public bool Done { get; set; } = false;
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
	}
}