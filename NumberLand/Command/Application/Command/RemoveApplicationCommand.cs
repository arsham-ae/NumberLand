﻿using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Command.Application.Command
{
    public class RemoveApplicationCommand : IRequest<CommandsResponse<ApplicationDTO>>
    {
        public int Id { get; set; }
        public RemoveApplicationCommand(int id)
        {
            Id = id;
        }
    }
}
