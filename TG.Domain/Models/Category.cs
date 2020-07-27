using System;
using System.Collections.Generic;

namespace TG.Domain.Models
{
	public class Category
	{
		private List<Question> _questions;
		public Category()
		{
			_questions = new List<Question>();
		}
		public int Id { get; set; }
		public string Name { get; set; }
		public List<Question> Questions
		{
			get => _questions;
			set => _questions = value ?? throw new ArgumentNullException(nameof(value));
		}
	}
}
