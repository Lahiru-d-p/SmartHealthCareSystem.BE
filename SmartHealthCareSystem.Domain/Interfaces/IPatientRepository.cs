﻿using SmartHealthCareSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHealthCareSystem.Domain.Interfaces;

public interface IPatientRepository
{
    Task<Patient> GetPatientByIdAsync(int id);
    //Task<IEnumerable<Patient>> GetAllPatientsAsync();
    //Task AddPatientAsync(Patient patient);
}
