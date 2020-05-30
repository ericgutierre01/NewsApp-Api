using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NewsAppApi.Entities;
using NewsAppApi.Entities.Data;
using NewsAppApi.Helpers;
using NewsAppApi.ViewModel;

namespace NewsAppApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuarioController : BaseController
    {
        private readonly ApiContext _db;
        private readonly AppSettings _appSettings;
        public UsuarioController(IOptions<AppSettings> appSettings, ApiContext context)
        {
            _appSettings = appSettings.Value;
            _db = context;
        }

        [AllowAnonymous]
        [HttpPost("/Admin/Login")]
        public ActionResult Login([FromBody] LoginViewModel args)
        {
            var usuario = _db.Usuarios.SingleOrDefault(x => x.UsuSesion.Equals(args.Usuario) &&
            x.UsuPass.Equals(Encryption.Encrypt(args.Password)));

            if (usuario == null) return BadRequest("Usuario o contraseña incorrecta!.");

            var res = new LoginResult();
            res.UsuId = usuario.UsuId;
            res.UsuNombre = usuario.UsuNombre;
            res.UsuApellido = usuario.UsuApellido;
            res.UsuSesion = usuario.UsuSesion;
            res.UsuEmail = usuario.UsuEmail;
            res.UsuEstado = usuario.UsuEstado;
            res.UsuFoto = usuario.UsuFoto;
            //res.UsuFoto = Path.Combine(_appSettings.DomainImg, "Avatar", usuario.UsuFoto);
            res.Token = GenerateToken(usuario);

            return Ok(res);
        }

        private string GenerateToken(Usuario args)
        {
            //Generar Token 
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Constantes.JWTSecret);
            var tokenExpires = DateTime.UtcNow.AddMonths(5);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, args.UsuSesion),
                    new Claim(ClaimTypes.NameIdentifier, args.UsuId.ToString()),
                }),
                Expires = tokenExpires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}