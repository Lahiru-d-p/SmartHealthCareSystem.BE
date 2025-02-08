using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SmartHealthCareSystem.Domain.Entities;
using SmartHealthCareSystem.Domain.Models;
using SmartHealthCareSystem.Infrastructure.Data;
using SmartHealthCareSystem.Infrastructure.Utilities;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public UsersController(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    
    [HttpPost("register/doctor")]
    public async Task<IActionResult> RegisterDoctor([FromBody] DoctorInsertModel model)
    {
        if (await _context.Doctors.AnyAsync(d => d.ContactEMail  == model.ContactEMail))
        {
            return BadRequest("Email already exists.");
        }

        var doctor = new Doctor
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            ContactEMail = model.ContactEMail,
            ContactNumber = model.ContactNumber,
            PasswordHash = PasswordHasherHelper.HashPassword(model.Password),
            Role = "Doctor",
            Address = model.Address,
            Specialty = model.Specialty,
            LicenseNumber = model.LicenseNumber,
            ClinicAddress = model.ClinicAddress,
        };

        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { id = doctor.Id }, doctor);
    }

    
    [HttpPost("register/patient")]
    public async Task<IActionResult> RegisterPatient([FromBody] PatientInsertModel model)
    {
        if (await _context.Patients.AnyAsync(d => d.ContactEMail == model.ContactEMail))
        {
            return BadRequest("Email already exists.");
        }

        var patient = new Patient
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            ContactEMail = model.ContactEMail,
            ContactNumber = model.ContactNumber,
            PasswordHash = PasswordHasherHelper.HashPassword(model.Password),
            Role = "Patient",
            Address = model.Address,
            DateOfBirth = model.DateOfBirth,
            MedicalHistory = model.MedicalHistory,

        };

        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { id = patient.Id }, patient);
    }

    
    [HttpGet("user/{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null) return NotFound();

        return Ok(user);
    }

    
    [HttpGet("doctor/{id}")]
    public async Task<IActionResult> GetDoctor(int id)
    {
        var doctor = await _context.Doctors.FindAsync(id);

        if (doctor == null) return NotFound();

        return Ok(doctor);
    }
    
    [HttpGet("patient/{id}")]
    public async Task<IActionResult> GetPatient(int id)
    {
        var patient = await _context.Patients.FindAsync(id);

        if (patient == null) return NotFound();

        return Ok(patient);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.ContactEMail == request.Email);
        if (user == null) return Unauthorized("User not found.");

        if (!PasswordHasherHelper.VerifyPassword(user.PasswordHash, request.Password))
            return Unauthorized("Invalid password.");

        var token = JwtHelper.GenerateToken(user.ContactEMail, user.Role, _configuration);

        return Ok(new
        {
            Token = token,
            UserId = user.Id,
            Role = user.Role
        });
    }


    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    // Book an appointment

    [HttpPost("book")]
    public async Task<IActionResult> BookAppointment([FromBody] AppointmentInsertModel request)
    {
        var appointment = new Appointment
        {
            AppointmentDate = request.AppointmentDateTime,
            FK_PatientId = request.FK_PatientId,
            FK_DoctorId = request.FK_DoctorId,
            Description = request.Description,
            Status = "Pending"
        };

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        return Ok("Appointment booked successfully.");
    }

    // Get appointments for a doctor
    [Authorize(Roles = "Doctor")]
    [HttpGet("doctor-appointments/{doctorId}")]
    public async Task<IActionResult> GetAppointmentsForDoctor(int doctorId)
    {
        var appointments = await _context.Appointments
            .Where(a => a.FK_DoctorId == doctorId)
            .ToListAsync();

        return Ok(appointments);
    }

    // Get appointments for a patient
    [Authorize(Roles = "Patient")]
    [HttpGet("patient-appointments/{patientId}")]
    public async Task<IActionResult> GetAppointmentsForPatient(int patientId)
    {
        var appointments = await _context.Appointments
            .Where(a => a.FK_PatientId == patientId)
            .ToListAsync();

        return Ok(appointments);
    }

}
