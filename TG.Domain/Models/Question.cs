using System;
using System.Collections.Generic;

namespace TG.Domain.Models
{
	public class Question
	{
		private List<Answer> _answers;
		public Question()
		{
			_answers = new List<Answer>();
		}
		public int Id { get; set; }
		public string Text { get; set; }

		public List<Answer> Answers
		{
			get => _answers;
			set => _answers = value ?? throw new ArgumentNullException(nameof(value));
		}
	}
}
