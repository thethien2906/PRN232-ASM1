﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HIV_CARE.Repositories.ThienTTT.Models;

public partial class DoctorPhatNh
{
    [Key]
    public int DoctorsPhatNhid { get; set; }

    [Required(ErrorMessage = "License number is required.")]
    [RegularExpression(@"^DOC\d{3}$", ErrorMessage = "License number must follow the format DOCXXX (e.g., DOC001).")]
    public string LicenseNumber { get; set; }

    [Required(ErrorMessage = "Specialization is required.")]
    public string Specialization { get; set; }

    [Required(ErrorMessage = "Years of experience are required.")]
    public int ExperienceYears { get; set; }

    public decimal ConsultationFee { get; set; }

    public bool IsAvailable { get; set; }

    public int MaxPatientsPerDay { get; set; }

    public DateTime AvailableStartDate { get; set; }

    public DateTime AvailableEndDate { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string ModifiedBy { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<AppointmentThienTtt> AppointmentThienTtts { get; set; } = new List<AppointmentThienTtt>();
}