using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TG.Application.Hub.Commands.Leave
{
	public class LeaveCommand : IRequest<int>
	{
		public string ConnectionId { get; set; }
	}
}
