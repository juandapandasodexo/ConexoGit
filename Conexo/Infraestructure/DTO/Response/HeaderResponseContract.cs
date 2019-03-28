using System.Collections.Generic;

namespace Infraestructure.DTO.Response
{
    public class HeaderResponseContract
	{
		public int Status { get; set; }
		public List<string>  Messages { get; set; }
		
	}
}
