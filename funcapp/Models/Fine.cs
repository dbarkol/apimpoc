using System;
using System.Collections.Generic;
using System.Text;

namespace funcapp.Models
{
	public class Fine
	{
		public string FirstName { get; set; }
		public double Amount { get; set; }
		public DateTime DateIssued { get; set; }
		public DateTime DateDue { get; set; }
	}
}
