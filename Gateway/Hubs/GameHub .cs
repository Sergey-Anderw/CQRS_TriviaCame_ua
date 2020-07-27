using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using TG.Application.Hub.Commands;
using TG.Application.Hub.Commands.Leave;
using TG.Domain.Interfaces.Hub;
using TG.Domain.Models;

namespace TG.Infrastructure.Hub
{
	public class GameHub : Hub<IGameClient>, IGameHub
	{
		private readonly ILogger<GameHub> _logger;
		private readonly IMediator _mediator;
		private IGameRepository _gamerepository;
		public GameHub(
			IMediator mediator,
			ILogger<GameHub> logger,
			IGameRepository gamerepository) 
		{
			if (mediator == null)
				throw new ArgumentNullException(nameof(mediator));
			_mediator = mediator;
			_logger = logger;
			_gamerepository = gamerepository;
		}

		public async Task Join(string characterColor)
		{
			var gameRoom = await _mediator.Send(
				new JoinCommand
				{
					CharacterColor = characterColor,
					ConnectionId = Context.ConnectionId
				});

			if (gameRoom is null)
				throw new ArgumentNullException(nameof(GameplayRoom));

			var player = gameRoom.Players.LastOrDefault();
			if (player is null)
				new ArgumentNullException(nameof(Player));

			await Groups.AddToGroupAsync(player.ConnectionId, gameRoom.Id.ToString());

			if (!player.IsGameOrganizer)
			{
				gameRoom.Players.ForEach(async p =>
				await Clients
				.GroupExcept(
					gameRoom.Id.ToString(),
					gameRoom.Players.First(x => x.ConnectionId == p.ConnectionId).ConnectionId)
				.OpponentJoined(p.Name, p.CharacterColor, p.IsGameOrganizer)
				);

				_gamerepository.GameRooms.Add(gameRoom);
				await Clients.Group(gameRoom.Id.ToString()).CanPlay();
			}
					
		}

		public async Task Leave()
		{
			await PlayerLeave();
		}

		public override async Task OnDisconnectedAsync(Exception exception)
		{
			await PlayerLeave();
			await base.OnDisconnectedAsync(exception);
		}
		public async Task Send(string jsonData)
		{
			var gr = _gamerepository.GameRooms
				.First(x => x.Players.Any(p => p.ConnectionId == Context.ConnectionId));
			if (gr is null)
				throw new ArgumentNullException(nameof(GameplayRoom));
			
			await Clients.GroupExcept(
				gr.Id.ToString(), Context.ConnectionId)
				.Send(jsonData);
		}

		private async Task PlayerLeave()
		{
			var connectionId = Context.ConnectionId;
			var gameplayRoom = _gamerepository.GameRooms
				.FirstOrDefault(gr => gr.Players.Any(p => p.ConnectionId == connectionId));
			if (gameplayRoom is null)
				throw new ArgumentNullException(nameof(GameplayRoom));

			await Groups.RemoveFromGroupAsync(connectionId, gameplayRoom.Id.ToString());
			await Clients.Group(gameplayRoom.Id.ToString()).OpponentLeave();
			_gamerepository.GameRooms.Remove(gameplayRoom);
			await _mediator.Send(
				new LeaveCommand
				{
					ConnectionId = Context.ConnectionId
				});
			Context.Abort();
		}
	}
}
