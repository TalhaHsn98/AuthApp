using AuthApi.Models;

namespace AuthApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;

        public AuthService(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            var existing = await _userRepository.GetByUserNameAsync(request.UserName);
            if (existing != null)
            {
                throw new InvalidOperationException("User already exists.");
            }

            _passwordHasher.CreateHash(request.Password, out var hash, out var salt);

            var user = new User
            {
                UserName = request.UserName,
                PasswordHash = hash,
                PasswordSalt = salt
            };

            await _userRepository.AddAsync(user);

            var token = _tokenService.GenerateToken(user);
            return new AuthResponse { Token = token };
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByUserNameAsync(request.UserName);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            var ok = _passwordHasher.Verify(request.Password, user.PasswordHash, user.PasswordSalt);
            if (!ok)
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            var token = _tokenService.GenerateToken(user);
            return new AuthResponse { Token = token };
        }
    }
}
