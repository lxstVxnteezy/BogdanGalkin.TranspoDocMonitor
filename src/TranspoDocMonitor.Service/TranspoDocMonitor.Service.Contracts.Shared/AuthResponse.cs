﻿

namespace TranspoDocMonitor.Service.Contracts.Shared
{
    public class AuthResponse
    {
        public string Token { get; }

        public AuthResponse(string token)
        {
            Token = token;
        }
    }
}
