using EstadoCuenta.Api.DTOs;
using FluentValidation;

namespace EstadoCuenta.Api.FluentValidation
{
    public class TransaccionDtoValidator : AbstractValidator<TransaccionDto>
    {
        public TransaccionDtoValidator()
        {
            RuleFor(x => x.Fecha)
            .NotEmpty().WithMessage("La fecha de la transacción es obligatoria.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha de la transacción no puede ser en el futuro.");

            RuleFor(x => x.Monto)
                .GreaterThan(0).WithMessage("El monto debe ser mayor a 0.");

            RuleFor(x => x.Descripcion)
                .NotEmpty().WithMessage("La descripción es obligatoria.")
                .MaximumLength(500).WithMessage("La descripción no puede superar los 500 caracteres.");

            RuleFor(x => x.IdTipoTransaccion)
                .GreaterThan(0).WithMessage("El ID del tipo de transacción debe ser mayor a 0.");

            RuleFor(x => x.NumTarjeta)
                .NotEmpty().WithMessage("El número de tarjeta es obligatorio.")
                .Length(16).WithMessage("El número de tarjeta debe tener 16 caracteres.");
        }
    }
}
