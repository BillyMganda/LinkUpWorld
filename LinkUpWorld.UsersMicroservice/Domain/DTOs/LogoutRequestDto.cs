﻿namespace LinkUpWorld.UsersMicroservice.Domain.DTOs
{
    public class LogoutRequestDto
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
