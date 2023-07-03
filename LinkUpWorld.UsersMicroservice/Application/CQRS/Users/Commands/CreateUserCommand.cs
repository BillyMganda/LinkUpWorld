using LinkUpWorld.UsersMicroservice.Application.CQRS.Users.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LinkUpWorld.UsersMicroservice.Application.CQRS.Users.Commands
{
    public class CreateUserCommand : IRequest<GetUserDto>
    {
        [Required(ErrorMessage = "The FirstName field is required.")]
        [MaxLength(50, ErrorMessage = "The FirstName field cannot exceed 50 characters.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "The LastName field is required.")]
        [MaxLength(50, ErrorMessage = "The LastName field cannot exceed 50 characters.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = string.Empty;
        
        [MaxLength(50, ErrorMessage = "The Bio field cannot exceed 50 characters.")]
        public string Bio { get; set; } = string.Empty;
        
        [MaxLength(500, ErrorMessage = "The Profile Picture field cannot exceed 500 characters.")]
        public string ProfilePicture { get; set; } = string.Empty;

        [Required(ErrorMessage = "The Password field is required.")]
        [MinLength(8, ErrorMessage = "The Password field must have a minimum of 8 characters.")]
        public string Password { get; set; } = string.Empty;
    }
}
