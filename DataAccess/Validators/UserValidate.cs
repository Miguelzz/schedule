using DataAccess.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Validators
{
    public class UserValidate : AbstractValidator<User>
    {
        public UserValidate()
        {
            RuleFor(t => t.Name)
                .NotEmpty()
                .Length(3, 20)
                .WithMessage("El nombre deve tener minimo 3 letras y maximo 20");

            RuleFor(t => t.LastName)
                .NotEmpty()
                .Length(3, 20)
                .WithMessage("El apellido deve tener minimo 3 letras y maximo 20");

            RuleFor(t => t.DocumentType)
                .IsInEnum()
                .WithMessage("Tipo de documento invalido!!");

            RuleFor(t => t.DocumentNumber)
                .NotEmpty()
                .Length(8, 10)
                .WithMessage("El documento debe tener maximo 10 numeros!!");

            RuleFor(t => t.Phone)
                .NotEmpty()
                .Length(10)
                .WithMessage("El celular debe 10 numeros!!");

            RuleFor(t => t.EMail)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("El correo es invalido!!");

        }
    }
}