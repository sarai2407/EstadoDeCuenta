using EstadoCuenta.Api.DTOs;
using FluentValidation;

namespace EstadoCuenta.Api.FluentValidation
{
    public class TipoTransaccionDtoValidator : AbstractValidator<TipoTransaccionDto>
    {
        public TipoTransaccionDtoValidator()
        {
            RuleFor(x => x.TipoDeTransaccion)
                .NotEmpty().WithMessage("El tipo de transaccion es obligatorio.")
                .MaximumLength(15).WithMessage("El nombre no puede tener más de 15 caracteres.");
        }
    }
}
