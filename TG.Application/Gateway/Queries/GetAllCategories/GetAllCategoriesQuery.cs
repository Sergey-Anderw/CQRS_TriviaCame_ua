using MediatR;
using System.Collections.Generic;
using TG.Domain.Models;

namespace TG.Application.Queries
{
	public class GetAllCategoriesQuery : IRequest<IEnumerable<Category>>
	{

	}

}
