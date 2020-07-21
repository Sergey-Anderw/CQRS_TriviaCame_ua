using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TG.Infrastructure.EF;

namespace TG.Application.Hub.Commands
{
	public class JoinCommandHandler : IRequestHandler<JoinCommand>
	{
		private readonly IMapper _mapper;
		private readonly ILogger<JoinCommandHandler> _logger;//TO DO
		private UnitOfWork _unitOfWork;
		public JoinCommandHandler(
			IMapper mapper,
			UnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		public async Task<Unit> Handle(JoinCommand request, CancellationToken cancellationToken)
		{
			using(_unitOfWork)
			{
				var connectionIds = await _unitOfWork.PlayerRepository.Get(p => p.ConnectionId == request.ConnectionId);
				if (connectionIds.Count() == 0)
					_unitOfWork.PlayerRepository.Create(new Domain.Entities.PlayerEntity());

			}
			return Unit.Value;
		}
	}
}
