using Application.Abstractions;
using Application.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Users
{
    public class Details
    {
        public class Query : IRequest<UserProfileDTO>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
        public class Handler : IRequestHandler<Query, UserProfileDTO>
        {
            private readonly IUsersRepository _repository;

            public Handler(IUsersRepository userRepository)
            {
                _repository = userRepository;
            }

            public Task<UserProfileDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                return _repository.SignIn(request.Email, request.Password);
            }
        }
    }
}