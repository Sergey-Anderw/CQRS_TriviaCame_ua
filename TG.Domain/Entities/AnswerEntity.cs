namespace TG.Domain.Entities
{
	public class AnswerEntity : EntityBase
	{
		public string Text { get; set; }
		public bool IsCorrect { get; set; }
		public int? QuestionId { get; set; }
		public QuestionEntity Question { get; set; }
	}
}
