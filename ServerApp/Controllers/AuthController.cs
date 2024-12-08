using Microsoft.AspNetCore.Mvc;
using ServerApp.Models.Domain;
using ServerApp.Models.DTO;
using ServerApp.Repositories.Abstract;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace ServerApp.Controllers{

 [Route("api/[controller]")]
    [ApiController]

    public class AuthController: ControllerBase{


        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;
        public AuthController(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

      //  [HttpPost("login")]   
       [HttpPost("login/doctor")]
public async Task<IActionResult> LoginDoctor([FromBody] UserForLoginDto loginDto)
{
    var doctor = await _authRepository.LoginDoctor(loginDto);
    if (doctor == null)
        return Unauthorized("Invalid credentials");

    // Generate JWT for doctor
    doctor.Token = CreateJWT(doctor);
    return Ok(doctor);
}

[HttpPost("login/patient")]
public async Task<IActionResult> LoginPatient([FromBody] UserForLoginDto loginDto)
{
    var patient = await _authRepository.LoginPatient(loginDto);
    if (patient == null)
        return Unauthorized("Invalid credentials");

    // Generate JWT for patient
    patient.Token = CreateJWT(patient);
    return Ok(patient);
}





private string CreateJWT(object user)
{
    var secretKey = _configuration.GetSection("AppSettings:key").Value;
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

    // Use dynamic typing to access properties on user
    dynamic dynamicUser = user;

    var claims = new Claim[]
    {
        new Claim(ClaimTypes.Name, dynamicUser.FirstName + " " + dynamicUser.LastName),
        new Claim(ClaimTypes.NameIdentifier, dynamicUser.Id.ToString()),
        new Claim(ClaimTypes.Role, dynamicUser.Role ?? ""),
    };

    var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddDays(1),
        SigningCredentials = signingCredentials
    };

    var tokenHandler = new JwtSecurityTokenHandler();
    var token = tokenHandler.CreateToken(tokenDescriptor);

    return tokenHandler.WriteToken(token);
}







        [HttpPost("register/doctor")]
        public async Task<IActionResult> RegisterDoctor([FromBody] DoctorDTO doctorDto)
        {
            var result = await _authRepository.RegisterDoctor(doctorDto);
            if (!result)
                return BadRequest("Doctor registration failed. Doctor may already exist.");

            return Ok("Doctor registered successfully");
        }

        [HttpPost("register/patient")]
        public async Task<IActionResult> RegisterPatient([FromBody] PatientDTO patientDto)
        {
            var result = await _authRepository.RegisterPatient(patientDto);
            if (!result)
                return BadRequest("Patient registration failed. Patient may already exist.");

            return Ok("Patient registered successfully");
        }
    }
}