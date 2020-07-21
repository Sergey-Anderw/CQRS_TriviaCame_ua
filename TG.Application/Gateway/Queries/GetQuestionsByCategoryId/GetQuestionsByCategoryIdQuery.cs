using MediatR;
using System.Collections.Generic;
using TG.Domain.Models;

namespace TG.Application.Queries
{
	public	class GetQuestionsByCategoryIdQuery : IRequest<IEnumerable<Question>>
	{
		public int Id { get; set; }
	}
}
