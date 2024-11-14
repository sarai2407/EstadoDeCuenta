using EstadoCuenta.Api.DTOs;
using FluentValidation;

namespace EstadoCuenta.Api.FluentValidation
{
    public class TarjetaDtoValidator : AbstractValidator<TarjetaDto>
    {
        public TarjetaDtoValidator()
        {
            RuleFor(x => x.NumTarjeta)
            .NotEmpty().WithMessage("El número de tarjeta es obligatorio.")
            .Length(16).WithMessage("El número de tarjeta debe tener 16 caracteres.");

            RuleFor(x => x.LimiteCredito)
                .GreaterThan(0).WithMessage("El límite de crédito debe ser mayor a 0.");

            RuleFor(x => x.Saldo)
                .GreaterThanOrEqualTo(0).WithMessage("El saldo no puede ser negativo.");

            RuleFor(x => x.SaldoDisponible)
                .GreaterThanOrEqualTo(0).WithMessage("El saldo disponible no puede ser negativo.")
                .Equal(x => x.LimiteCredito - x.Saldo)
                .WithMessage("El saldo disponible debe ser igual al límite de crédito menos el saldo.");

            RuleFor(x => x.IdUsuario)
                .NotEmpty().WithMessage("El número de tarjeta es obligatorio.")
                .GreaterThan(0).WithMessage("El ID de usuario debe ser mayor a 0.");

        }
    }
}
