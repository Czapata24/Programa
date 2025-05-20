using HankoSpa.Data;
using HankoSpa.Data.Seeder;
using HankoSpa.Services.Interfaces;

namespace HankoSpa.Models
{
    public class SeedDb
    {
        private readonly AppDbContext _context;
        private readonly IUsersService _userServices;


        public SeedDb (AppDbContext context, IUsersService usersService)
        {
            _context = context;
            _userServices = usersService;
        }

        public async Task SeedAsync()
        {
            await new PermissionsSeeder(_context).SeedAsync();
            await new UserRolesSeeder(_context, _userServices).SeedAsync();

        }

    }

}
