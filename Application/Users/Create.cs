using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Users
{
    public class Create
    {
        public class Command : IRequest<int>
        {
            public NewUserDTO NewUser { get; set; }
        }

        public class Handler : IRequestHandler<Command, int>
        {
            private readonly IUsersRepository _userRepository;

            public Handler(IUsersRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                return _userRepository.CreateUser(request.NewUser);
            }
        }
    }
}