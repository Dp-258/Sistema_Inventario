using Microsoft.AspNetCore.Identity;

namespace MVCInventario
{
    public class SpanishIdentityErrorDescriber : IdentityErrorDescriber
    {
       
            public override IdentityError PasswordRequiresDigit()
            {
                return new IdentityError { Code = nameof(PasswordRequiresDigit), Description = "La contraseña debe contener al menos un dígito ('0'-'9')." };
            }

            public override IdentityError PasswordRequiresLower()
            {
                return new IdentityError { Code = nameof(PasswordRequiresLower), Description = "La contraseña debe contener al menos una letra minúscula ('a'-'z')." };
            }

            public override IdentityError PasswordRequiresUpper()
            {
                return new IdentityError { Code = nameof(PasswordRequiresUpper), Description = "La contraseña debe contener al menos una letra mayúscula ('A'-'Z')." };
            }

            public override IdentityError PasswordRequiresNonAlphanumeric()
            {
                return new IdentityError { Code = nameof(PasswordRequiresNonAlphanumeric), Description = "La contraseña debe contener al menos un carácter especial ('@', '#', '$', etc.)." };
            }

            // Agregar otros métodos de acuerdo a su preferencia
        
    }
}
