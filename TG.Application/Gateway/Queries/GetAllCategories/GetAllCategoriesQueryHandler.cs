using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using TG.Infrastructure.EF;
using Microsoft.Extensions.Logging;
using AutoMapper;
using TG.Domain.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TG.Application.Queries.GetAllCategories
{
	public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<Category>>
	{
		private readonly IMapper _mapper;
		private readonly ILogger<GetAllCategoriesQueryHandler> _logger;//TO DO
		private UnitOfWork _unitOfWork;
		public GetAllCategoriesQueryHandler(
			IMapper mapper,
			UnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}
		public async Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery query, CancellationToken cancellationToken)
		{
			using(_unitOfWork)
		    {
				var categories = await _unitOfWork.CategoryRepository.Get(null, source => source
					.Include(c => c.Questions)
					.ThenInclude(x => x.Answers));
				return _mapper.Map<List<Category>>(categories.ToList());
			}			
		}
	}
}
