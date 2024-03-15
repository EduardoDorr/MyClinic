using MyClinic.Common.ValueObjects;

namespace MyClinic.Common.Models.InputModels;

public sealed record BloodDataInputModel(BloodType BloodType, RhFactor RhFactor);