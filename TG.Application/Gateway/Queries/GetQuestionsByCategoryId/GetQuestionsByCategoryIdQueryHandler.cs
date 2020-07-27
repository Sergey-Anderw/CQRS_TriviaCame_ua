using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TG.Domain.Entities;
using TG.Domain.Models;
using TG.Infrastructure.EF;

namespace TG.Application.Queries.GetQuestionsByCategoryId
{
	public class GetQuestionsByCategoryIdQueryHandler : IRequestHandler<GetQuestionsByCategoryIdQuery, Question>
	{
		private readonly IMapper _mapper;
		private readonly ILogger<GetQuestionsByCategoryIdQuery> _logger;//TO DO
		private UnitOfWork _unitOfWork;

		public GetQuestionsByCategoryIdQueryHandler(
			IMapper mapper,
			UnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}
		public async Task<Question> Handle(GetQuestionsByCategoryIdQuery request, CancellationToken cancellationToken)
		{
			using (_unitOfWork)
			{
				var categoryEntity = await _unitOfWork.CategoryRepository.GetById(
					request.Id, source => source
					.Include(c => c.Questions)
					.ThenInclude(a => a.Answers));

				if (categoryEntity == null)
					throw new ArgumentNullException(nameof(QuestionEntity));

				var rnd = new Random();
				var question = categoryEntity.Questions.OrderBy(s => rnd.NextDouble()).First();
				return _mapper.Map<Question>(question);
			}
		}
	}
}
