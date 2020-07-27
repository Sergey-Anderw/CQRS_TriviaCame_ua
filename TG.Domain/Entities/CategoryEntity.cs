using System;
using System.Collections.Generic;

namespace TG.Domain.Entities
{
	public class CategoryEntity : EntityBase
	{
		private List<QuestionEntity> _questions;
		public CategoryEntity()
		{
			_questions = new List<QuestionEntity>();
		}
		public string Name { get; set; } 
		public List<QuestionEntity> Questions 
		{
			get => _questions;
			set => _questions = value ?? throw new ArgumentNullException(nameof(value));
		}

	}
}
