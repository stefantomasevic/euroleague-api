using Euroleague.Data;
using Euroleague.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Euroleague.Authorization
{
    public class AuthorizationRepository : IAuthorizationRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;


        public AuthorizationRepository(ApplicationDbContext context,IPasswordHasher passwordHasher )
        {
            _context = context;
            _passwordHasher = passwordHasher;

        }


        public async Task<bool> RegisterUserAsync(Register model)
        {
            if (await _context.Admins.AnyAsync(u => u.Username == model.Username))
            {
                return false; // Korisnik već postoji
            }

            // Hesiraj lozinku
            var hashedPassword = _passwordHasher.HashPassword( model.Password);

            // Kreiraj novog korisnika
            var newUser = new Admin
            {
                Username = model.Username,
                PasswordHash = hashedPassword
            };

            // Dodaj korisnika u bazu
            _context.Admins.Add(newUser);
            await _context.SaveChangesAsync();

            return true; // Registracija uspešna
        }

        public async Task<Admin> ValidateUserAsync(Login model)
        {
            var user = await _context.Admins.SingleOrDefaultAsync(u => u.Username == model.Username);

            // Proveri da li korisnik postoji i da li lozinka odgovara
            if (user != null && _passwordHasher.VerifyPassword(user.PasswordHash, model.Password))
            {
                return user; // Korisnik je validan
            }

            return null; // Neuspela validacija
        }

    }
}
