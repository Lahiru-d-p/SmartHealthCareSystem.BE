using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartHealthCareSystem.Domain.Entities;
using SmartHealthCareSystem.Infrastructure.Data;
using SmartHealthCareSystem.Infrastructure.Utilities;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("register/doctor")]
    public async Task<IActionResult> RegisterDoctor([FromBody] Doctor doctor)
    {
        doctor.PasswordHash = PasswordHasherHelper.HashPassword(doctor.PasswordHash);
        doctor.Role = "Doctor";

        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { id = doctor.Id }, doctor);
    }

    [HttpPost("register/patient")]
    public async Task<IActionResult> RegisterPatient([FromBody] Patient patient)
    {
        patient.PasswordHash = PasswordHasherHelper.HashPassword(patient.PasswordHash);
        patient.Role = "Patient";

        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { id = patient.Id }, patient);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null) return NotFound();

        return Ok(user);
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.ContactEMail == request.Email);
        if (user == null) return Unauthorized("User not found.");

        if (!PasswordHasherHelper.VerifyPassword(user.PasswordHash, request.Password))
            return Unauthorized("Invalid password.");

        return Ok(new { Message = "Login successful", UserId = user.Id, Role = user.Role });
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
