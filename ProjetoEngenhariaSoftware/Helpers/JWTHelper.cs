using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace ProjetoEngenhariaSoftware.Helpers
{
    public static class JWTHelper
    {
        #region Generate
        //public static string generate(User user, Equipment equipment = null)
        //{
        //    JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();
        //    byte[] secretKey = System.Text.ASCIIEncoding.ASCII.GetBytes(ConfigurationManager.AppSettings["JWTSecretKey"].ToString());

        //    DateTime now = DateTime.Now;
        //    DateTime issuedTime = now;
        //    DateTime notBefore = now;

        //    double minutes = 1440;
        //    try
        //    {
        //        minutes = (double)new AppSettingsReader().GetValue("TempoExpiraToken", double.MinValue.GetType());
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.AddError(MethodBase.GetCurrentMethod(), "Erro ao buscar TempoExpiraToken do config :" + ex.Message);
        //    }
        //    DateTime expireTime = now.AddMinutes(minutes);

        //    List<Claim> paramValues = new List<Claim>();
        //    paramValues.Add(new Claim("UserID", user.Id.ToString()));
        //    paramValues.Add(new Claim("User", user.Name));
        //    paramValues.Add(new Claim("Role", user.Group.Name));
        //    paramValues.Add(new Claim("ClientId", user.Client == null ? null : user.Client.Id.ToString()));
        //    paramValues.Add(new Claim("Client", user.Client.Name));
        //    paramValues.Add(new Claim("Permissions", "{[{'Monitoramento'},{'Equipamentos'}]}"));
        //    paramValues.Add(new Claim("IP", System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_HOST"]));
        //    if(equipment != null) { 
        //        paramValues.Add(new Claim("EquipmentID", equipment.Id.ToString()));
        //        if (equipment.Local != null)
        //            paramValues.Add(new Claim("LocalID", equipment.Local.Id.ToString()));
        //    }

        //    IEnumerable<Claim> claimList = paramValues;

        //    JwtPayload payload = new JwtPayload(
        //                                            issuer: "*",
        //                                            audience: ConfigurationManager.AppSettings["Audience"],
        //                                            claims: claimList,
        //                                            notBefore: notBefore,
        //                                            expires: expireTime/*,
        //                                            issu: issuedTime*/
        //                                        );

        //    SigningCredentials credencials = new SigningCredentials(new SymmetricSecurityKey(secretKey), System.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);

        //    JwtHeader header = new JwtHeader(credencials);

        //    JwtSecurityToken d = new JwtSecurityToken(header, payload);

        //    return _tokenHandler.WriteToken(d);
        //}

        //public static string generate(Equipment equipment)
        //{
        //    JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();
        //    byte[] secretKey = System.Text.ASCIIEncoding.ASCII.GetBytes(ConfigurationManager.AppSettings["JWTSecretKey"].ToString());

        //    DateTime now = DateTime.Now;
        //    DateTime issuedTime = now;
        //    DateTime notBefore = now;

        //    double minutes = 1440;
        //    try
        //    {
        //        minutes = (double)new AppSettingsReader().GetValue("TempoExpiraToken", double.MinValue.GetType());
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.AddError(MethodBase.GetCurrentMethod(), "Erro ao buscar TempoExpiraToken do config :" + ex.Message);
        //    }
        //    DateTime expireTime = now.AddMinutes(minutes);


        //    List<Claim> paramValues = new List<Claim>();
        //    paramValues.Add(new Claim("EquipmentID", equipment.Id.ToString()));
        //    if (equipment.Local != null)
        //        paramValues.Add(new Claim("LocalID", equipment.Local.Id.ToString()));
        //    if (equipment.Client != null)
        //        paramValues.Add(new Claim("ClientId", equipment.Client.Id.ToString()));

        //    IEnumerable<Claim> claimList = paramValues;

        //    JwtPayload payload = new JwtPayload(
        //                                            issuer: "*",
        //                                            audience: ConfigurationManager.AppSettings["Audience"],
        //                                            claims: claimList,
        //                                            notBefore: notBefore,
        //                                            expires: expireTime/*,
        //                                            issuedAt: issuedTime*/
        //                                        );

        //    SigningCredentials credencials = new SigningCredentials(new SymmetricSecurityKey(secretKey), Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);

        //    JwtHeader header = new JwtHeader(credencials);

        //    JwtSecurityToken d = new JwtSecurityToken(header, payload);

        //    return _tokenHandler.WriteToken(d);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nome"></param>
        /// <returns></returns>
        public static string Generate(string nome)
        {
            JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();
            byte[] secretKey = ASCIIEncoding.ASCII.GetBytes(ConfigurationManager.AppSettings["JWTSecretKey"].ToString());

            DateTime now = DateTime.Now;
            DateTime notBefore = now;

            double minutes = 1440;
            try
            {
                minutes = (double)new AppSettingsReader().GetValue("TempoExpiraToken", double.MinValue.GetType());
            }
            catch (Exception ex)
            {
            }

            DateTime expireTime = now.AddMinutes(minutes);

            List<Claim> paramValues = new List<Claim>();
            paramValues.Add(new Claim("Login", nome)); 
            paramValues.Add(new Claim("TimeStamp", DateTime.Now.ToString()));  

            IEnumerable<Claim> claimList = paramValues;
            JwtPayload payload = new JwtPayload(
                                issuer: "*",
                                audience: ConfigurationManager.AppSettings["Audience"],
                                claims: claimList,
                                notBefore: notBefore,
                                expires: expireTime 
                            );

            SigningCredentials credencials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);
            JwtHeader header = new JwtHeader(credencials);
            JwtSecurityToken d = new JwtSecurityToken(header, payload);
            return _tokenHandler.WriteToken(d);
        }
        #endregion

        #region Validate
        public static int validate(string jwtToken)
        {
            byte[] secretKey = System.Text.Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["JWTSecretKey"].ToString());
            SymmetricSecurityKey symmetricKey = new SymmetricSecurityKey(secretKey);

            JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();
            TokenValidationParameters parameters = new TokenValidationParameters();
            parameters.ValidIssuer = "*";
            parameters.ValidAudience = ConfigurationManager.AppSettings["Audience"];
            parameters.IssuerSigningKey = symmetricKey;
            SecurityToken jwtTokenValidated;
            
            
            try
            {
                _tokenHandler.ValidateToken(jwtToken, parameters, out jwtTokenValidated);
                return 1;
            }
            catch (Exception ex)
            {
                if (ex.TargetSite.Name.Equals("ValidateLifetime"))
                {
                    return 2;
                }
                
                return 0;
            }
        }

        public static bool validateRequisition(int localId, JwtSecurityToken token)
        {
            int tokenLocFirstId = 0;
            if (!String.IsNullOrEmpty(token.Claims.First(claim => claim.Type == "LocalID").Value))
                tokenLocFirstId = Convert.ToInt32(token.Claims.First(claim => claim.Type == "LocalID").Value);

            return localId == tokenLocFirstId;
        }
        #endregion
        

        public static JwtSecurityToken decrypt(string EncriptedToken)
        {
            JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();
            
            try
            {
                return _tokenHandler.ReadJwtToken(EncriptedToken);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
}