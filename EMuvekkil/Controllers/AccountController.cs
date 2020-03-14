using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EMuvekkil.Models;
using EMuvekkil.Models.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EMuvekkil.Controllers
{

    [Route("api/[controller]")]
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _singInManager;
        private IPasswordValidator<ApplicationUser> _passwordValidator;
        private IPasswordHasher<ApplicationUser> _passwordHasher;
        private ICompanyRepository _companyRepository;
        private IDavaRepository _davaRepository;
        private IConfiguration _configuration;
        private IMapper _mapper;
        private RoleManager<IdentityRole> _roleManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IPasswordHasher<ApplicationUser> passwordHasher,
            IPasswordValidator<ApplicationUser> passwordValidator,
            IMapper mapper, ICompanyRepository companyRepository, IDavaRepository davaRepository,
            RoleManager<IdentityRole> roleManager
            )
        {
            _userManager = userManager;
            _singInManager = signInManager;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
            _passwordValidator = passwordValidator;
            _mapper = mapper;
            _companyRepository = companyRepository;
            _davaRepository = davaRepository;
            _roleManager = roleManager;
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterUser([FromBody]RegisterViewModel registerView, Role role)
        {
            var user = new ApplicationUser { Email = registerView.Email, UserName = registerView.Email, IdentityNumber = registerView.IdentityNumber, NameSurname = registerView.NameSurname };
            if (registerView.CompanyId != 0)
            {
                var comp = _companyRepository.GetCompany(registerView.CompanyId);
                user.Company = comp;
            }
            var identityResult = await _userManager.CreateAsync(user, registerView.Password);
            if (identityResult.Succeeded)
            {
                identityResult = await _userManager.AddToRoleAsync(user, role.ToString());

                return Ok();
            }

            return BadRequest(identityResult.Errors);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteUser([FromBody]RegisterViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
                return Ok();
            }
            return BadRequest("Böyle bir kullanıcı bulunamadı");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateUser([FromBody]RegisterViewModel registerView)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(registerView.Id);
                if (user != null)
                {
                    user.Email = registerView.Email;
                    user.UserName = registerView.Email;
                    user.IdentityNumber = registerView.IdentityNumber;
                    user.NameSurname = registerView.NameSurname;

                    if (registerView.CompanyId != 0)
                    {
                        var comp = _companyRepository.GetCompany(registerView.CompanyId);
                        user.Company = comp;
                    }

                    if (registerView.Password != null)
                    {
                        IdentityResult validResult = await _passwordValidator.ValidateAsync(_userManager, user, registerView.Password);
                        if (validResult.Succeeded)
                        {
                            user.PasswordHash = _passwordHasher.HashPassword(user, registerView.Password);
                        }
                    }

                    var result = await _userManager.UpdateAsync(user);
                    return Ok(registerView);
                }
                return BadRequest("Böyle bir kullanıcı bulunmamaktadır");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var result = await _singInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (result.Succeeded)
                {

                    string role = "";
                    var isInRole = (await _userManager.IsInRoleAsync(user, Role.admin.ToString()));
                    if (isInRole) { role = Role.admin.ToString(); }
                    isInRole = (await _userManager.IsInRoleAsync(user, Role.avukat.ToString()));
                    if (isInRole) { role = Role.avukat.ToString(); }
                    isInRole = (await _userManager.IsInRoleAsync(user, Role.muvekkil.ToString()));
                    if (isInRole) { role = Role.muvekkil.ToString(); }

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_configuration["Application:Secret"]);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Email,user.Email),
                            new Claim(ClaimTypes.Role,role),
                            new Claim("nameSurname",user.NameSurname),
                            new Claim("id",user.Id),
                            new Claim("identityNumber",user.IdentityNumber)

                        }),
                        Expires = DateTime.UtcNow.AddDays(7),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);

                    return Ok(new { Token = tokenString });
                }
                else
                {
                    return BadRequest("Kullanıcı adı veya parola hatalı lütfen terkrar deneyiniz.");
                }
            }
            else
            {
                return BadRequest("Geçersiz Kullanıcı adı veya parola");
            }

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetLawyers()
        {
            var users = await _userManager.GetUsersInRoleAsync(Role.avukat.ToString());
            var avukats = _mapper.Map<IList<ApplicationUser>, IList<UserViewModel>>(users);
            return Ok(avukats);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetMuvekkils()
        {
            var users = new List<ApplicationUser>();
            var tempusers = _userManager.Users.Include(u => u.Company);

            foreach (var item in tempusers)
            {
                if (await _userManager.IsInRoleAsync(item, Role.muvekkil.ToString()))
                {
                    users.Add(item);
                }
            }


            var muvekkils = _mapper.Map<IList<ApplicationUser>, IList<UserViewModel>>(users);
            return Ok(muvekkils);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userManager.Users.Include(d => d.Company).SingleAsync(d => d.Id == id);

            if (user != null)
            {
                var userVM = _mapper.Map<UserViewModel>(user);

                return Ok(userVM);
            }
            return BadRequest("Böyle bir kullanıcı bulunamadı.");

        }

        [HttpGet("[action]")]
        public int GetUserDependencies(string id, Role role)
        {
            if (role == Role.avukat)
            {
                return _davaRepository.GetDavas().Where(d => d.AvukatId == id).Count();
            }
            else
            {
                return _davaRepository.GetDavas().Where(d => d.MuvekkilId == id).Count();
            }

        }


        [HttpGet("[action]")]
        public IActionResult GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            var usersVM = _mapper.Map<IList<UserViewModel>>(users);
            return Ok(usersVM);
        }



    }
}