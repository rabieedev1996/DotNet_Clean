using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Clean.Identity
{
    public class TokenParams
    {
        /// <summary>
        /// Required
        /// </summary>
        public string Key { set; get; }
        /// <summary>
        /// Required
        /// </summary>
        public string UserId { set; get; }
        /// <summary>
        /// Required
        /// </summary>
        /// Based on the values ​​of the SecurityAlgorithms in .NET
        /// For example SecurityAlgorithms.HmacSha256 is equal to "HS256"
        public string SecurityAlgorithm { set; get; }
        /// <summary>
        /// Required
        /// </summary>
        public TimeSpan ExpireTime { set; get; }
        /// <summary>
        /// Optional
        /// </summary>
        public string Audience {  set; get; }
        /// <summary>
        /// Optional
        /// </summary>
        public string Issuer {  set; get; }
        /// <summary>
        /// Optional
        /// </summary>
        public string Reason {  set; get; } 
        /// <summary>
        /// Optiona
        /// </summary>
        public List<string> Roles {  set; get; }
        /// <summary>
        /// Required
        /// </summary>
        public string TokenId {  set; get; }
        /// <summary>
        /// Optional
        /// </summary>
        public List<KeyValuePair<string, string>> OtherClaims { set; get; }  
    }
}
