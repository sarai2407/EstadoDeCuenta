using EstadoCuenta.Api.DTOs;
using FluentValidation;

namespace EstadoCuenta.Api.FluentValidation
{
    public class UsuarioDtoValidator : AbstractValidator<UsuarioDto>
    {
        public UsuarioDtoValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(50).WithMessage("El nombre no puede tener más de 50 caracteres.");

            RuleFor(x => x.Apellidos)
                .NotEmpty().WithMessage("El apellido es obligatorio.")
                .MaximumLength(50).WithMessage("El apellido no puede tener más de 50 caracteres.");

            RuleFor(x => x.Telefono)
                .NotEmpty().WithMessage("El teléfono es obligatorio.")
                .Matches(@"^\d{8}$").WithMessage("El teléfono debe tener 8 dígitos.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es obligatorio.")
                .EmailAddress().WithMessage("El email debe tener un formato válido.");

            RuleFor(x => x.Dui)
                .NotEmpty().WithMessage("El DUI es obligatorio.")
                .Matches(@"^\d{8}-\d{1}$").WithMessage("El DUI debe tener el formato 00000000-0.");
        }
    }
}
