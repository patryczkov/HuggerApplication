using Hugger_Application.Models.TokenModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Services.TokenServices.TokenManager
{
    public class TokenManager : ITokenManager
    {
        private readonly IDistributedCache _cache;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptions<JWTOptions> _jwtOptions;

        public TokenManager(IDistributedCache cache, IHttpContextAccessor httpContextAccessor, IOptions<JWTOptions> jwtOptions)
        {
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;
            _jwtOptions = jwtOptions;
        }

        public async Task<bool> IsActiveAsync(string token)
         => await _cache.GetStringAsync(GetKey(token)) == null;


        public async Task<bool> IsCurrentActiveToken()
           => await IsActiveAsync(GetCurrentAsync());

        public async Task DeactivateCurrentAsync()
          => await DeactivateAsync(GetCurrentAsync());



        public async Task DeactivateAsync(string token)
             => await _cache.SetStringAsync(GetKey(token),
                 " ", new DistributedCacheEntryOptions
                 {
                     AbsoluteExpirationRelativeToNow =
                         TimeSpan.FromMinutes(_jwtOptions.Value.ExpiryMinutes)
                 });
        private string GetCurrentAsync()
        {
            var authorizationHeader = _httpContextAccessor
                .HttpContext.Request.Headers["authorization"];

            return authorizationHeader == StringValues.Empty
                ? string.Empty
                : authorizationHeader.Single().Split(" ").Last();
        }

        private static string GetKey(string token)
            => $"tokens:{token}:deactivated";
    }
}

