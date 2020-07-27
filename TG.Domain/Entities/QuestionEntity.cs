using System;
using System.Collections.Generic;

namespace TG.Domain.Entities
{
	public class QuestionEntity : EntityBase
	{
		private List<AnswerEntity> _answers;
		public QuestionEntity()
		{
			_answers = new List<AnswerEntity>();
		}
		public string Text { get; set; }
		public int? CategoryId { get; set; }
		public CategoryEntity Category { get; set; }

		public List<AnswerEntity> Answers 
		{
			get => _answers;
			set => _answers = value ?? throw new ArgumentNullException(nameof(value));
		}
	}
}