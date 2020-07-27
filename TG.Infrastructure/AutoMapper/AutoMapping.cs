using AutoMapper;
using System.Linq;
using TG.Domain.Entities;
using TG.Domain.Models;

namespace TG.Infrastructure.AutoMapper
{
	public class AutoMapping : Profile
	{
		public AutoMapping()
		{
			CreateMap<CategoryEntity, Category>();
			CreateMap<QuestionEntity, Question>();
			CreateMap<AnswerEntity, Answer>();
			CreateMap<PlayerEntity, Player>();
			CreateMap<GameplayRoomEntity, GameplayRoom>();
		}
	}
}
