using LinkUpWorld.UsersMicroservice.Application.CQRS.Users.Commands;
using LinkUpWorld.UsersMicroservice.Application.Exceptions;
using LinkUpWorld.UsersMicroservice.Domain.Repositories;
using MediatR;

namespace LinkUpWorld.UsersMicroservice.Application.CQRS.Users.Handlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new CustomValidationException("command is required.");
            }

            try
            {
                var user = await _userRepository.GetByIdAsync(request.Id);

                if (user == null)
                {
                    throw new NotFoundException($"User with ID {request.Id} not found.");
                }

                await _userRepository.DeleteAsync(request.Id);

                return Unit.Value;
            }
            catch (NotFoundException)
            {
                throw new NotFoundException($"User with ID {request.Id} not found.");
            }
            catch (Exception ex)
            {
                throw new CustomException("An error occurred while deleting a user.", ex);
            }
        }
    }
}
