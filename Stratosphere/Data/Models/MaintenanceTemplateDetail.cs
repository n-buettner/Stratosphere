﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace Stratosphere.Data.Models;

[PrimaryKey(nameof(MaintenanceTemplateId), nameof(MaintenanceTemplateDetailId))]
public class MaintenanceTemplateDetail
{
    [Required] public Guid? MaintenanceTemplateId { get; set; }
    [Required] public int MaintenanceTemplateDetailId { get; set; }
    [Required] public string? CreatedBy { get; set; }
    [Required] public DateTime? CreatedDate { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    [Required] public Guid? ServiceId { get; set; }
    [Required] public Guid? EnvironmentId { get; set; }
    [Required] public int StartOrder { get; set; }
    [Required] public int StopOrder { get; set; }
    public bool WaitForQueueClearOnStart { get; set; }
    public bool WaitForQueueClearOnStop { get; set; }
}

public class MaintenanceTemplateDetailConfiguration : IEntityTypeConfiguration<MaintenanceTemplateDetail>
{
    public void Configure(EntityTypeBuilder<MaintenanceTemplateDetail> builder)
    {

    }
}