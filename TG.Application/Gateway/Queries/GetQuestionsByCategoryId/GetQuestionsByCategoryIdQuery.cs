using MediatR;
using System.Collections.Generic;
using TG.Domain.Models;

namespace TG.Application.Queries
{
	public	class GetQuestionsByCategoryIdQuery : IRequest<Question>
	{
		public int Id { get; set; }
	}
}
