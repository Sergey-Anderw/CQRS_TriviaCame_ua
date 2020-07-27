using System;
using System.Collections.Generic;
using System.Text;

namespace TG.Domain.Models
{
	public class Answer
	{
		public int Id { get; set; }
		public string Text { get; set; }
		public bool IsCorrect { get; set; }
	}
}
