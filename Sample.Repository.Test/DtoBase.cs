using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPractice.Repository.Test
{
	public class DtoBase
	{
		public int Time { get; set; }
	}

	public class ProductDto	  : DtoBase
	{
		public int ProductId { get; set; }

		public string Description { get; set; }

	}
}
